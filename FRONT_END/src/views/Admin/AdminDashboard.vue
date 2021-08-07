<template>
  <div class="admin-dashboard">
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
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>

    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "AdminDashboard",
  data() {
    return {
      // Bindings for the app bar component
      drawer: true,

      // Binding All links
      items: [
        {
          icon: "equalizer",
          title: "Statistics",
          route: "/AdminDashboard/AppStats",
        },
        { icon: "groups", title: "Users", route: "/AdminDashboard/Users" },
        {
          icon: "group",
          title: "Managers",
          route: "/AdminDashboard/Managers",
        },
      ],
      accountRouteObj: [
        { title: "Settings", route: "/Settings" },
        { title: "Sign out" },
      ],
    };
  },
};
</script>
