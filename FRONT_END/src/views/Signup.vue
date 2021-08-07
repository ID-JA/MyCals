<template>
  <div class="signup">
    <!-- Error in signup snackbar -->
    <v-snackbar v-model="snackbarSignupError">
      {{ errorMessage }}
      <template v-slot:action="{ attrs }">
        <v-btn
          color="pink"
          text
          v-bind="attrs"
          @click="snackbarSignupError = false"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
    <Navbar />
    <v-container>
      <h2 class="primary--text text-center">Sign Up</h2>
      <v-form
        @submit.prevent="signUpHandle"
        method="POST"
        class="d-block mx-auto"
        style="max-width: 400px"
        ref="form"
      >
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              label="Firstname"
              v-model="newUser.FirstName"
              prepend-icon="badge"
              :rules="name"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              label="Lastname"
              v-model="newUser.LastName"
              prepend-icon="badge"
              :rules="name"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6">
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
                  prepend-icon="mdi-calendar"
                  v-bind="attrs"
                  @blur="date = parseDate(dateFormatted)"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="newUser.Date_Of_Birth"
                no-title
                @input="menu1 = false"
              ></v-date-picker>
            </v-menu>
          </v-col>
          <v-col cols="12" md="6">
            <v-select
              :items="items"
              label="Gender"
              :rules="genderRule"
              v-model="newUser.Gender"
            ></v-select>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12">
            <v-text-field
              label="Email"
              v-model="newUser.Email"
              prepend-icon="email"
              :rules="emailRule"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              label="Password"
              v-model="newUser.Password"
              prepend-icon="lock"
              :rules="passwordRule"
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              label="Confirm password"
              prepend-icon="lock"
              v-model="newUser.ConfirmPassword"
              :rules="confirmPasswordRule"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-btn
          depressed
          class="primary my-4 text-capitalize"
          block
          @click="signUpHandle"
          :loading="signupButtonLoading"
          >Sign up</v-btn
        >
        <p class="text-center">
          Already have an account?
          <router-link to="/Login" class="font-weight-bold primary--text"
            >Login in</router-link
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
  name: "Signup",
  components: {
    Navbar,
  },
  data(vm) {
    return {
      snackbarSignupError: false,
      errorMessage:
        "Registration failed. Please double check your information!",
      newUser: {
        FirstName: "",
        LastName: "",
        Email: "",
        Password: "",
        Gender: "",
        ConfirmPassword: "",
        Date_Of_Birth: "",
        Role: "User",
      },
      signupButtonLoading: false,

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
      name: [
        function (name) {
          let nameRegex = new RegExp("^[a-zA-Z]+([ a-zA-Z]+)?$");
          if (!nameRegex.test(name)) {
            return "The name must contain characters only";
          }
        },
      ],
      confirmPasswordRule: [
        (password) => password == this.newUser.Password || "Password not match",
      ],
      genderRule: [
        (gender) => gender.length >= 0 || "Please select your gender",
      ],
      items: ["Male", "Female"],
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
    signUpHandle() {
      this.signupButtonLoading = true;
      if (this.$refs.form.validate()) {
        createApiEndPoints(END_POINTS.AUTH_REGISTER)
          .create({ ...this.newUser })
          .then(() => {
            this.signupButtonLoading = false;
            this.$router.push("Login");
          })
          .catch((error) => console.log(error));
      } else {
        this.signupButtonLoading = false;
        this.snackbarSignupError = true;
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

<style lang="scss" scoped>
.login {
  margin-top: 4rem;
}
</style>
