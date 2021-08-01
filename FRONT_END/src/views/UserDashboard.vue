<template>
  <div class="dashboard">
    <v-app-bar app>
      <v-app-bar-nav-icon
        class="grey--text"
        @click="drawer = !drawer"
      ></v-app-bar-nav-icon>
      <v-toolbar-title class="text-uppercase grey--text">
        <span class="">My</span>
        <span class="primary--text">Cals</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>

      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-img
            src="https://source.unsplash.com/random/50x50"
            max-width="40"
            class="rounded-circle"
            v-bind="attrs"
            v-on="on"
          ></v-img>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in itemsd"
            :key="index"
            link
            :to="item.route"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" class="primary white--text" app>
      <div class="subheading white--text text-center my-16 dashboard__app-name">
        MyCals
      </div>

      <v-list nav>
        <v-list-item-group v-model="drawer">
          <v-list-item
            v-for="item in items"
            :key="item.title"
            link
            :to="item.route"
          >
            <v-list-item-icon>
              <v-icon class="white--text">{{ item.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title class="white--text">{{
                item.title
              }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list>

      <!-- <v-list nav>
        <v-list-item-group v-model="drawer">
          <v-list-item
            link
          >
            <v-list-item-icon>
              <v-icon class="white--text">star</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title class="white--text">Rate us</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list-item-group>
      </v-list> -->
    </v-navigation-drawer>

    <v-container>
      <!-- searching -->
      <v-row>
          <v-col cols="12" sm="6" md="3">
              <v-text-field
        label="Search By Meal"
        v-model="searchingMeal"
      ></v-text-field>
          </v-col>
      </v-row>
      <!-- Table -->
      <v-data-table
        :headers="headers"
        :items="desserts"
        :items-per-page="5"
        class="elevation-1 my-4"
      ></v-data-table>
      <v-dialog v-model="dialog" width="500">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          Add new meal
        </v-btn>
      </template>

      <v-card>
        <v-card-title class="primary white--text">
          Add new meal
        </v-card-title>

        <v-card-text
          >
          <v-form ref="form">
            <v-text-field
            label="number of calories"
              v-model="numCals"
              :rules="numCalsRules"
            ></v-text-field>
            <v-textarea
              label="description"
              v-model="description"
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
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="mealDate"
                no-title
                @input="menu1 = false"
              ></v-date-picker>
            </v-menu>
            <v-time-picker
              ampm-in-title
              format="24hr"
              scrollable
              v-model="time"
            ></v-time-picker>
          </v-form>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            text
            class="text-lowercase"
          >
            Add
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "UserDashboard",
  data(vm) {
    return {
      searchingMeal: "",
      numCals: "",
      description: "",
      mealDate: "",
      time: "",
      drawer: null,
      items: [
        { icon: "equalizer", title: "Statistics", route: "/" },
        { icon: "restaurant", title: "Meals", route: "/Meals" },
        { icon: "help_outline", title: "FAQ", route: "/Faq" },
        { icon: "star", title: "Rate us", route: "/Rateus" },
      ],
      itemsd: [
        { title: "Settings", route: "/Settings" },
        { title: "Sign out", route: "/Signout" },
      ],
      headers: [
        {
          text: "Dessert (100g serving)",
          align: "start",
          sortable: false,
          value: "name",
        },
        { text: "Calories", value: "calories" },
        { text: "Fat (g)", value: "fat" },
        { text: "Carbs (g)", value: "carbs" },
        { text: "Protein (g)", value: "protein" },
        { text: "Iron (%)", value: "iron" },
      ],
      desserts: [
        {
          name: "Frozen Yogurt",
          calories: 159,
          fat: 6.0,
          carbs: 24,
          protein: 4.0,
          iron: "1%",
        },
        {
          name: "Ice cream sandwich",
          calories: 237,
          fat: 9.0,
          carbs: 37,
          protein: 4.3,
          iron: "1%",
        },
        {
          name: "Eclair",
          calories: 262,
          fat: 16.0,
          carbs: 23,
          protein: 6.0,
          iron: "7%",
        },
        {
          name: "Cupcake",
          calories: 305,
          fat: 3.7,
          carbs: 67,
          protein: 4.3,
          iron: "8%",
        },
        {
          name: "Gingerbread",
          calories: 356,
          fat: 16.0,
          carbs: 49,
          protein: 3.9,
          iron: "16%",
        },
        {
          name: "Jelly bean",
          calories: 375,
          fat: 0.0,
          carbs: 94,
          protein: 0.0,
          iron: "0%",
        },
        {
          name: "Lollipop",
          calories: 392,
          fat: 0.2,
          carbs: 98,
          protein: 0,
          iron: "2%",
        },
        {
          name: "Honeycomb",
          calories: 408,
          fat: 3.2,
          carbs: 87,
          protein: 6.5,
          iron: "45%",
        },
        {
          name: "Donut",
          calories: 452,
          fat: 25.0,
          carbs: 51,
          protein: 4.9,
          iron: "22%",
        },
        {
          name: "KitKat",
          calories: 518,
          fat: 26.0,
          carbs: 65,
          protein: 7,
          iron: "6%",
        },
      ],
      // Rules
      numCalsRules: [(value) => (!isNaN(value) && value.length > 0) || "Please enter a numeric value"],
      descriptionRules: [(desc) => desc.length > 0 || "Description is required"],
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
  },

  methods: {
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

<style lang="scss" scoped></style>
