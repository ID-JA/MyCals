<template>
  <div class="meals">
    <v-container>
      <!-- searching -->
      <v-row wrap class="align-center">
        <!-- searching input col -->
        <v-col cols="12" md="3">
          <v-text-field
            label="Search By Meal"
            v-model="searchingMeal"
            prepend-icon="search"
            @keyup="getSearchedMeals(searchingMeal)"
          ></v-text-field>
        </v-col>
        <!-- spicer -->
        <v-col cols="12" md="6"></v-col>
        <!-- add button col -->
        <v-col cols="12" md="3" class="d-flex align-center justify-end">
          <v-dialog v-model="addMealDialog" width="500">
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                depressed
                class="primary white--text"
                v-bind="attrs"
                v-on="on"
              >
                Add new meal
              </v-btn>
            </template>

            <v-card>
              <v-card-title class="text-h6 primary white--text">
                Adding new meal
              </v-card-title>

              <v-card-text>
                <v-form ref="formAdd">
                  <v-text-field
                    label="meal name"
                    v-model="newMealObj.mealName"
                    :rules="mealNameRule"
                  ></v-text-field>
                  <v-textarea
                    label="description"
                    v-model="newMealObj.mealDescription"
                    :rules="descriptionRules"
                  ></v-textarea>
                  <!-- Date picker menu -->
                  <v-menu
                    ref="menu1"
                    v-model="menu1"
                    :close-on-content-click="false"
                    transition="scale-transition"
                    offset-y
                    max-width="290px"
                    min-width="auto"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="dateFormatted"
                        label="Date"
                        hint="MM/DD/YYYY format"
                        persistent-hint
                        v-bind="attrs"
                        @blur="date = parseDate(dateFormatted)"
                        v-on="on"
                        class="mb-6"
                        :rules="mealDateRule"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      v-model="newMealObj.mealDate"
                      no-title
                      @input="menu1 = false"
                    ></v-date-picker>
                  </v-menu>
                  <v-time-picker
                    ampm-in-title
                    format="24hr"
                    scrollable
                    v-model="newMealObj.mealTime"
                    class="time-picker"
                    :rules="mealTimeRule"
                  ></v-time-picker>
                  <v-text-field
                    label="Calories"
                    v-model="newMealObj.mealCalories"
                    :rules="mealCaloriesRules"
                  ></v-text-field>
                </v-form>
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-btn
                  color="primary"
                  text
                  @click="
                    addMealDialog = false;
                    addLoading = false;
                  "
                >
                  Cancel
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn
                  color="primary"
                  text
                  :loading="addLoading"
                  @click="addMeal(newMealObj)"
                >
                  Add meal
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-col>
      </v-row>
      <!-- Table -->
      <!-- In Case there is no searching -->
      <v-container>
        <div v-if="!searching">
          <v-row wrap v-for="(meal, index) in meals" :key="meal.id">
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Meal</div>
              <div>{{ meal.mealName }} index: {{ index }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Description</div>
              <div>{{ meal.mealDescription }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Date</div>
              <div>{{ meal.mealDate }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Time</div>
              <div class="">{{ meal.mealTime }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Calories</div>
              <div class="">{{ meal.mealCalories }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Actions</div>
              <!-- Dialog button for Delete -->
              <div class="">
                <v-dialog v-model="deleteMealDialog" width="500">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn icon depressed v-bind="attrs" v-on="on">
                      <v-icon>mdi-delete</v-icon>
                    </v-btn>
                  </template>

                  <v-card>
                    <v-card-title
                      class="text-h5 primary white--text"
                      style="word-wrap: break-word"
                    >
                      Are You Sure You Want To Delete This Meal?
                    </v-card-title>

                    <v-divider></v-divider>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        text
                        @click="deleteMealDialog = false"
                      >
                        No
                      </v-btn>
                      <v-btn color="primary" text @click="deleteMeal(index)">
                        Yes, delete it!
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div>
              <!-- Dialog button for Update -->
              <div class="">
                <v-dialog v-model="updateMealDialog" width="500">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn icon depressed v-bind="attrs" v-on="on">
                      <v-icon>mdi-pencil</v-icon>
                    </v-btn>
                  </template>

                  <v-card>
                    <v-card-title class="text-h5 primary white--text">
                      Edit Meal
                    </v-card-title>

                    <!-- Card Body -->
                    <v-card-text>
                      <v-form ref="form">
                        <v-text-field
                          label="meal name"
                          v-model="editMealObj.mealName"
                          :rules="mealNameRule"
                        ></v-text-field>
                        <v-textarea
                          label="description"
                          v-model="editMealObj.mealDescription"
                          :rules="descriptionRules"
                        ></v-textarea>
                        <!-- Date picker menu -->
                        <v-menu
                          ref="menu1"
                          v-model="menu1"
                          :close-on-content-click="false"
                          transition="scale-transition"
                          offset-y
                          max-width="290px"
                          min-width="auto"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              v-model="dateFormatted"
                              label="Date"
                              hint="MM/DD/YYYY format"
                              persistent-hint
                              v-bind="attrs"
                              @blur="date = parseDate(dateFormatted)"
                              v-on="on"
                              class="mb-6"
                              :rules="mealDateRule"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            v-model="editMealObj.mealDate"
                            no-title
                            @input="menu1 = false"
                          ></v-date-picker>
                        </v-menu>
                        <v-time-picker
                          ampm-in-title
                          format="24hr"
                          scrollable
                          v-model="editMealObj.mealTime"
                          class="time-picker"
                          :rules="mealTimeRule"
                        ></v-time-picker>
                        <v-text-field
                          label="Calories"
                          v-model="editMealObj.mealCalories"
                          :rules="mealCaloriesRules"
                        ></v-text-field>
                      </v-form>
                    </v-card-text>

                    <v-divider></v-divider>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        text
                        @click="updateMealDialog = false"
                      >
                        Cancel
                      </v-btn>
                      <v-btn
                        color="primary"
                        text
                        @click="updateMeal(editMealObj, index)"
                      >
                        Save
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div>
            </v-col>
          </v-row>
        </div>
        <div v-else>
          <v-row wrap v-for="(meal, index) in searchedMeals" :key="meal.id">
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Meal</div>
              <div>{{ meal.mealName }} index: {{ index }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Description</div>
              <div>{{ meal.mealDescription }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Date</div>
              <div>{{ meal.mealDate }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Time</div>
              <div class="">{{ meal.mealTime }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Calories</div>
              <div class="">{{ meal.mealCalories }}</div>
            </v-col>
            <v-col cols="12" sm="4" md="2">
              <div class="caption grey--text">Actions</div>
              <!-- Dialog button for Delete -->
              <div class="">
                <v-dialog v-model="deleteMealDialog" width="500">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn icon depressed v-bind="attrs" v-on="on">
                      <v-icon>mdi-delete</v-icon>
                    </v-btn>
                  </template>

                  <v-card>
                    <v-card-title
                      class="text-h5 primary white--text"
                      style="word-wrap: break-word"
                    >
                      Are You Sure You Want To Delete This Meal?
                    </v-card-title>

                    <v-divider></v-divider>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        text
                        @click="deleteMealDialog = false"
                      >
                        No
                      </v-btn>
                      <v-btn color="primary" text @click="deleteMeal(index)">
                        Yes, delete it!
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div>
              <!-- Dialog button for Update -->
              <div class="">
                <v-dialog v-model="updateMealDialog" width="500">
                  <template v-slot:activator="{ on, attrs }">
                    <v-btn icon depressed v-bind="attrs" v-on="on">
                      <v-icon>mdi-pencil</v-icon>
                    </v-btn>
                  </template>

                  <v-card>
                    <v-card-title class="text-h5 primary white--text">
                      Edit Meal
                    </v-card-title>

                    <!-- Card Body -->
                    <v-card-text>
                      <v-form ref="form">
                        <v-text-field
                          label="meal name"
                          v-model="editMealObj.mealName"
                          :rules="mealNameRule"
                        ></v-text-field>
                        <v-textarea
                          label="description"
                          v-model="editMealObj.mealDescription"
                          :rules="descriptionRules"
                        ></v-textarea>
                        <!-- Date picker menu -->
                        <v-menu
                          ref="menu1"
                          v-model="menu1"
                          :close-on-content-click="false"
                          transition="scale-transition"
                          offset-y
                          max-width="290px"
                          min-width="auto"
                        >
                          <template v-slot:activator="{ on, attrs }">
                            <v-text-field
                              v-model="dateFormatted"
                              label="Date"
                              hint="MM/DD/YYYY format"
                              persistent-hint
                              v-bind="attrs"
                              @blur="date = parseDate(dateFormatted)"
                              v-on="on"
                              class="mb-6"
                              :rules="mealDateRule"
                            ></v-text-field>
                          </template>
                          <v-date-picker
                            v-model="editMealObj.mealDate"
                            no-title
                            @input="menu1 = false"
                          ></v-date-picker>
                        </v-menu>
                        <v-time-picker
                          ampm-in-title
                          format="24hr"
                          scrollable
                          v-model="editMealObj.mealTime"
                          class="time-picker"
                          :rules="mealTimeRule"
                        ></v-time-picker>
                        <v-text-field
                          label="Calories"
                          v-model="editMealObj.mealCalories"
                          :rules="mealCaloriesRules"
                        ></v-text-field>
                      </v-form>
                    </v-card-text>

                    <v-divider></v-divider>

                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        text
                        @click="updateMealDialog = false"
                      >
                        Cancel
                      </v-btn>
                      <v-btn
                        color="primary"
                        text
                        @click="updateMeal(editMealObj, index)"
                      >
                        Save
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
              </div>
            </v-col>
          </v-row>
        </div>
      </v-container>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "Meals",
  data(vm) {
    return {
      // Bindings for ALL meals
      meals: [
        {
          mealID: 0,
          mealName: "meal 101",
          mealDescription: "desc 101",
          mealDate: "08/10/2021",
          mealTime: "13:33",
          mealCalories: 101,
        },
        {
          mealID: 1,
          mealName: "meal 102",
          mealDescription: "desc 102",
          mealDate: "08/10/2021",
          mealTime: "13:2",
          mealCalories: 202,
        },
      ],
      // The searching meals array
      searchedMeals: [],
      searching: false,
      // meal buttons Dialog
      addMealDialog: false,
      deleteMealDialog: false,
      updateMealDialog: false,
      //Loaders
      loader: null,
      addLoading: false,
      updateLoading: false,
      deleteLoading: false,
      // data binding for the add button
      newMealObj: {
        mealID: 0,
        mealName: "",
        mealDescription: "",
        mealDate: "",
        mealTime: "",
        mealCalories: 0,
      },

      // Data binding for the edit button
      editMealObj: {
        mealID: 0,
        mealName: "",
        mealDescription: "",
        mealDate: "",
        mealTime: "",
        mealCalories: 0,
      },

      // The searching meal
      searchingMeal: "",

      // Add Meal Input Rules
      mealNameRule: [(name) => name.length > 0 || "Meal name is required"],
      descriptionRules: [
        (description) => description.length > 0 || "Description is required",
      ],
      mealDateRule: [(mealDate) => mealDate.length > 0 || "Date is required"],
      mealTimeRule: [(mealTime) => mealTime.length > 0 || "Time is required"],
      mealCaloriesRules: [
        (cals) =>
          (!isNaN(cals) && cals.length > 0) ||
          "Calories must be a numeric value",
      ],

      // Date picker
      date: new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
        .toISOString()
        .substr(0, 10),
      dateFormatted: vm.formatDate(
        new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
          .toISOString()
          .substr(0, 10)
      ),
      menu1: false,
      menu2: false,
    };
  },
  computed: {
    computedDateFormatted() {
      return this.formatDate(this.date);
    },
  },
  watch: {
    date() {
      this.dateFormatted = this.formatDate(this.date);
    },
    // Button loaders
    loader() {
      const l = this.loader;
      this[l] = !this[l];

      setTimeout(() => (this[l] = false), 3000);

      this.loader = null;
    },
  },
  methods: {
    // Searching for a meal
    getSearchedMeals(value) {
      this.searching = true;
      let copiedMealsArr = [...this.meals];
      let searchingItems = copiedMealsArr.filter((mealObj) =>
        mealObj.mealName.includes(value)
      );
      this.searchedMeals = [...searchingItems];
    },

    // Meal CRUD
    addMeal(meal) {
      this.addLoading = !this.addLoading;
      if (this.$refs.formAdd.validate()) {
        this.meals.push(meal);
        this.addMealDialog = false;
        console.log(this.meals);
        this.newMealObj = {
          mealID: 0,
          mealName: "",
          mealDescription: "",
          mealDate: "",
          mealTime: "",
          mealCalories: 0,
        };
        this.searching = false;
      }
    },

    deleteMeal(index) {
      console.log(index);
      this.meals.splice(index, 1);
      console.log(this.meals);
      this.deleteMealDialog = false;
    },

    formatDate(date) {
      if (!date) return null;
      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;
      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    },
  },
};
</script>
