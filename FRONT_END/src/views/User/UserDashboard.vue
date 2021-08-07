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
            style="cursor: pointer"
            v-bind="attrs"
            v-on="on"
          ></v-img>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in accountRouteObj"
            :key="index"
            link
            @click="handleSignout"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" class="primary white--text" app>
      <div
        class="
          subheading
          white--text
          text-center
          my-16
          dashboard__app-name dashboard-title
        "
      >
        MyCals
      </div>

      <v-list nav shaped>
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
          <v-dialog v-model="Ratingdialog" width="500">
            <template v-slot:activator="{ on, attrs }">
              <v-list-item v-bind="attrs" v-on="on">
                <v-list-item-icon>
                  <v-icon class="white--text">star</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  <v-list-item-title class="white--text"
                    >Rate us</v-list-item-title
                  >
                </v-list-item-content>
              </v-list-item>
            </template>

            <v-card>
              <v-card-title class="text-h5 primary white--text">
                Share your success story
              </v-card-title>

              <v-card-text>
                <v-rating
                  v-model="rating"
                  color="primary"
                  hover
                  length="5"
                  size="25"
                ></v-rating>
                <v-form ref="opinionForm">
                  <v-textarea
                    v-model="opinion"
                    :rules="opinionRule"
                    label="Opinion"
                  ></v-textarea>
                </v-form>
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="primary"
                  text
                  :loading="rateusLoading"
                  @click="Ratingdialog = false"
                >
                  Cancel
                </v-btn>
                <v-btn
                  color="primary"
                  text
                  :loading="rateusLoading"
                  @click="rateApp()"
                >
                  Share
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "UserDashboard",
  data() {
    return {
      // Bindings for the app bar component
      drawer: true,
      // Rating data
      Ratingdialog: false,
      opinion: "",
      rating: 0,
      rateusLoading: false,

      // Binding All links
      items: [
        { icon: "restaurant", title: "Meals", route: "/userDashboard/Meals" },
        {
          icon: "equalizer",
          title: "Statistics",
          route: "/userDashboard/UserStats",
        },
      ],
      accountRouteObj: [
        { title: "Settings", route: "/Settings" },
        { title: "Sign out", route: "/Signout" },
      ],
      // Rule for description length
      opinionRule: [
        (value) =>
          value.length >= 20 || "Opinion must be 20 characters or more",
      ],
    };
  },
  methods: {
    // Rating the app functionality
    rateApp() {
      if (this.$refs.opinionForm.validate() && this.rating > 0) {
        this.rateusLoading = true;
        console.log(this.rating + "" + this.opinion);
        this.rateusLoading = false;
        this.Ratingdialog = false;
      }
    },

    // Sign out
    handleSignout() {
      console.log("entered");
      localStorage.removeItem("L_T");
      this.$router.push("/");
    },
  },
};
</script>

<style lang="scss" scoped>
.time-picker {
  left: 50%;
  transform: translateX(-50%);
}
</style>
