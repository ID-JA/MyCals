<template>
  <div class="login">
    <Navbar />
    <v-container>
      <h2 class="primary--text text-center">Log in</h2>
      <v-form @submit.prevent="loginHandle" class="d-block mx-auto" style="max-width: 400px">
        <v-text-field
          label="Email"
          v-model="loggedinUser.Email"
          prepend-icon="email"
          :rules="emailRule"
        ></v-text-field>
        <v-text-field
          label="Password"
          v-model="loggedinUser.Password"
          prepend-icon="lock"
          :rules="passwordRule"
        ></v-text-field>
        <router-link to="" class="font-weight-bold"
          >Forgot password?</router-link
        >
        <v-btn depressed class="primary my-4 text-capitalize" block :loading="loginButtonLoading" @click="loginHandle"
          >Log in</v-btn
        >
        <p class="text-center">
          Don't have an account?
          <router-link to="/Signup" class="font-weight-bold primary--text"
            >Sign up</router-link
          >
        </p>
      </v-form>
    </v-container>
  </div>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import { END_POINTS, createApiEndPoints } from "@/api.js";

export default {
  name: "Login",
  components: {
    Navbar,
  },
  data() {
    return {
      loggedinUser: {
        Email: "",
        Password: "",
      },
      emailRule: [
        function (email) {
          let emailRegex = new RegExp(
            "^[a-zA-Z0-9]+((._-)[a-zA-Z0-9]+)?@(gmail|yahoo|hotmail).(com|fr|uk|net)$"
          );
          if (!emailRegex.test(email)) {
            return "please enter a valid email adresse";
          }
        },
      ],
      passwordRule: [
        (password) =>
          password.length >= 2 || "Password must have at least 8 characters",
      ],
      loginButtonLoading: false,
    };
  },

  methods: {
    loginHandle() {
      
      createApiEndPoints(END_POINTS.AUTH_LOGIN)
        .create({...this.loggedinUser})
        .then((response) => {

          // Get the token
          if(response.status === 200) {
            this.$store.dispatch("setToken", response.data.userDisplay);
            console.log(this.$store.state.userDisplay);
            // this.$router.push("/userDashboard");
          }

        })
        .then(error => console.log(error));

    }
  }
};
</script>

<style lang="scss" scoped>
.login {
  margin-top: 4rem;
}
</style>
