<template>
  <div class="managerDashboard">
    <v-app-bar app>
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
            :to="item.route"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-container>
        <!-- Searching for a user -->
      <v-row>
        <v-col cols="12" sm="6" lg="4">
          <v-text-field
            label="Search by name"
            prepend-icon="search"
            v-model="searchingUser"
          >
          </v-text-field>
        </v-col>
      </v-row>
    </v-container>
    <!-- Table data -->
    <!-- id, fname, lname, dob, avatar, height, weight, email, password -->
    <v-container>
        <v-row v-for="user in users" :key="user.id">
            <v-col cols="12" sm="2">
                <div class="caption grey--text">Firstname</div>
                <div>{{ user.firstName }}</div>
            </v-col>
            <v-col cols="12" sm="2">
                <div class="caption grey--text">Lastname</div>
                <div>{{ user.lastName }}</div>
            </v-col>
            <v-col cols="12" sm="2">
                <div class="caption grey--text">Date of birth</div>
                <div>{{ user.dob }}</div>
            </v-col>
            <v-col cols="12" sm="2">
                <div class="caption grey--text">height</div>
                <div>{{ user.height }}</div>
            </v-col>
            <v-col cols="12" sm="1">
                <div class="caption grey--text">weight</div>
                <div>{{ user.weight }}</div>
            </v-col>
            <v-col cols="12" sm="1">
                <div class="caption grey--text">Avatar</div>
                <div>{{ user.avatar }}</div>
            </v-col>
            <v-col cols="12" sm="2">
                <div class="caption grey--text">Actions</div>
                <div class="d-flex align-center justify-space-around">
                    <div>edit</div>
                <div>delete</div>
                </div>
            </v-col>
        </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "managerDashboard",
  data() {
    return {
      accountRouteObj: [
        { title: "Settings", route: "/Settings" },
        { title: "Sign out", route: "/Signout" },
      ],
      searchingUser: "",
      users: [
          {
              id: 1,
              firstName: "Anas",
              lastName: "Mellas",
              dob: "23/05/2001",
              height: 0,
              weight: 0,
              avatar: "avatar"
          },
          {
              id: 2,
              firstName: "Jamal",
              lastName: "Idaissa",
              dob: "23/05/2001",
              height: 0,
              weight: 0,
              avatar: "avatar"
          },
      ]
    };
  },
};
</script>
