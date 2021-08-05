<template>
  <div class="login">
    <!-- Error in Login Snackbar -->
    <v-snackbar
      v-model="loginSnackbar"
    >
      {{ errorMessage }}

      <template v-slot:action="{ attrs }">
        <v-btn
          color="pink"
          text
          v-bind="attrs"
          @click="loginSnackbar = false"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
    <Navbar />
    <v-container>
      <h2 class="primary--text text-center">Log in</h2>
      <v-form ref="loginForm" @submit.prevent="loginHandle" class="d-block mx-auto" style="max-width: 400px">
        <v-text-field
          label="Email"
          v-model="loggedinUser.Email"
          prepend-icon="email"
          :rules="emailRule"
        ></v-text-field>
        <v-text-field
            v-model="loggedinUser.Password"
            prepend-icon="lock"
            :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
            :rules="passwordRule"
            :type="show1 ? 'text' : 'password'"
            name="input-10-1"
            label="Password"
            hint="At least 8 characters"
            counter
            @click:append="show1 = !show1"
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
      loginSnackbar: false,
      errorMessage: "Login failed, please verify your information",
      show1: false,
        show2: true,
        show3: false,
        show4: false,
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
          password.length >= 8 || "Password must have at least 8 characters",
      ],
      loginButtonLoading: false,
    };
  },

  computed: {
    getCredentials() {
      return this.$store.state.currentUserCredentials;
    }
  },

  methods: {
    loginHandle() {
      this.loginButtonLoading = true;
      if(this.$refs.loginForm.validate()) {

        createApiEndPoints(END_POINTS.AUTH_LOGIN)
        .create({...this.loggedinUser})
        .then((response) => {
          this.loginButtonLoading = false;
          // Get the token
          if(response.status === 200) {
            this.$store.dispatch("getUserCredentials", {...response.data.userDisplay});
            this.$router.push("userDashboard");
          }

        })
        .then(error => console.log(error));

      } else {

        this.loginButtonLoading = false;
        this.loginSnackbar = true;

      }

    }
  }
};
</script>

<style lang="scss" scoped>
.login {
  margin-top: 4rem;
}
</style>
