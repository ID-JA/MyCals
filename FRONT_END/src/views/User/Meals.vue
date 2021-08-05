<template>
  <div class="meals">
    <v-container>
      <!-- Success on add/edit operation -->
      <v-snackbar v-model="snackbarSuccess">
        {{ snackbarSuccessMessage }}

        <template v-slot:action="{ attrs }">
          <v-btn
            class="white--text"
            text
            v-bind="attrs"
            @click="snackbarSuccess = false"
          >
            Close
          </v-btn>
        </template>
      </v-snackbar>
      <!-- error on add/edit operation -->
      <v-snackbar v-model="snackbarError">
        {{ snackbarErrorMessage }}

        <template v-slot:action="{ attrs }">
          <v-btn
            class="white--text"
            text
            v-bind="attrs"
            @click="snackbarError = false"
          >
            Close
          </v-btn>
        </template>
      </v-snackbar>
      <!-- searching -->
      <v-row wrap class="align-center">
        <!-- searching input col -->
        <v-col cols="12" md="3">
          <v-text-field
            label="Search By Meal"
            v-model="searchingMeal"
            prepend-icon="search"
          ></v-text-field>
        </v-col>
        <!-- spicer -->
        <v-col cols="12" md="6"></v-col>
        <!-- add button col -->
      </v-row>
      <!-- Table -->
      <v-container>
        <v-data-table
          :headers="headers"
          :items="meals"
          sort-by="calories"
          class="elevation-1"
          :search="searchingMeal"
        >
          <template v-slot:top>
            <v-toolbar flat>
              <!-- <v-toolbar-title>My CRUD</v-toolbar-title>
              <v-divider class="mx-4" inset vertical></v-divider> -->
              <v-spacer></v-spacer>
              <v-dialog v-model="dialog" max-width="500px">
                <template v-slot:activator="{ on, attrs }">
                  <v-btn
                    color="primary"
                    dark
                    class="mb-2"
                    v-bind="attrs"
                    v-on="on"
                  >
                    New Meal
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="text-h5">{{ formTitle }}</span>
                  </v-card-title>

                  <v-card-text>
                    <v-container>
                      <v-form ref="mealsForm">
                        <v-row>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.Name"
                              label="Meal name"
                              :rules="mealNameRule"
                            ></v-text-field>
                          </v-col>
                          <v-col cols="12" sm="6">
                            <v-text-field
                              v-model="editedItem.Calories"
                              label="Calories"
                              :rules="mealCaloriesRules"
                            ></v-text-field>
                          </v-col>
                        </v-row>
                        <v-textarea
                          v-model="editedItem.Description"
                          label="description"
                          :rules="descriptionRules"
                        ></v-textarea>
                        <v-row justify="center">
                          <v-col class="d-flex justify-center">
                            <v-date-picker
                              v-model="editedItem.Date"
                            ></v-date-picker>
                          </v-col>
                          <v-col class="d-flex justify-center">
                            <v-time-picker
                              v-model="editedItem.Time"
                              format="24hr"
                            ></v-time-picker>
                          </v-col>
                        </v-row>
                      </v-form>
                    </v-container>
                  </v-card-text>

                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="close">
                      Cancel
                    </v-btn>
                    <v-btn color="blue darken-1" text @click="save">
                      Save
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>
              <v-dialog v-model="dialogDelete" max-width="500px">
                <v-card>
                  <v-card-title class="text-h5"
                    >Are you sure you want to delete this meal?</v-card-title
                  >
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="closeDelete"
                      >Cancel</v-btn
                    >
                    <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                      >Delete</v-btn
                    >
                    <v-spacer></v-spacer>
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-toolbar>
          </template>
          <template v-slot:item.actions="{ item }">
            <v-icon small class="mr-2" @click="editItem(item)">
              mdi-pencil
            </v-icon>
            <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
          </template>
          <template v-slot:no-data>
            <v-btn color="primary" @click="initialize"> Reset </v-btn>
          </template>
        </v-data-table>
      </v-container>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "Meals",
  data(vm) {
    return {
      // The searching meal
      searchingMeal: "",
      snackbarSuccess: false,
      snackbarError: false,
      snackbarSuccessMessage: "The operation is successfully done!",
      snackbarErrorMessage: "Error. Something went wrong!",
      dialog: false,
      dialogDelete: false,
      headers: [
        {
          text: "name",
          align: "start",
          value: "Name",
          filterable: true,
        },
        { text: "description", value: "Description", filterable: false },
        { text: "date", value: "Date", filterable: false },
        { text: "time", value: "Time", filterable: false },
        { text: "Calories", value: "Calories", filterable: false },
        { text: "Actions", value: "actions", filterable: false },
      ],
      meals: [],
      editedIndex: -1,
      editedItem: {
        Id_Meal: null,
        Name: "",
        Description: "",
        Date: null,
        Time: null,
        Calories: 0,
        Id_User: 0,
      },
      defaultItem: {
        Id_Meal: null,
        Name: "",
        Description: "",
        Date: null,
        Time: null,
        Calories: 0,
        Id_User: 0,
      },

      // Meal Input Rules
      mealNameRule: [
        (name) => (name.length > 0 && isNaN(name)) || "Meal name is required",
      ],
      descriptionRules: [
        (description) => description.length > 0 || "Description is required",
      ],
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
    formTitle() {
      return this.editedIndex === -1 ? "New Meal" : "Edit Meal";
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
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  created() {
    this.initialize();
  },

  methods: {
    initialize() {
      this.meals = [
        {
          Id_Meal: 1,
          Name: "Frozen Yogurt",
          Description: "description A",
          Date: new Date().toLocaleDateString("en-US"),
          Time: new Date().getHours() + ":" + new Date().getMinutes(),
          Calories: 159,
          Id_User: 1,
        },
        {
          Id_Meal: 2,
          Name: "Frozen Yogurt",
          Description: "description A",
          Date: new Date().toLocaleDateString("en-US"),
          Time: new Date().getHours() + ":" + new Date().getMinutes(),
          Calories: 159,
          Id_User: 1,
        },
      ];
    },

    editItem(item) {
      this.editedIndex = this.meals.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.meals.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.meals.splice(this.editedIndex, 1);
      // API
      this.closeDelete();
      this.snackbarSuccess = true;
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    save() {
      if (this.$refs.mealsForm.validate()) {
        if (this.editedIndex > -1) {
          Object.assign(this.meals[this.editedIndex], this.editedItem);
        } else {
          this.meals.push(this.editedItem);
        }
        this.close();
        // Success toolbar
        this.snackbarSuccess = true;
      } else {
        // Error Toolbar
        this.snackbarError = true;
      }
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
