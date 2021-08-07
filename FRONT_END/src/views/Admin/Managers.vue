<template>
  <div class="managers">
    <v-container>
      <!-- Searching for a user -->
      <v-row align="center" justify="space-between">
        <v-col cols="12" sm="6" lg="4">
          <v-text-field
            label="Search by name"
            prepend-icon="search"
            v-model="searchingUser"
          >
          </v-text-field>
        </v-col>
        <v-col cols="12" sm="6" lg="4">
          <v-dialog
            v-model="dialogAdd"
            width="500"
            class="d-inline-block ml-auto"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn color="primary" dark v-bind="attrs" v-on="on">
                New Manager
              </v-btn>
            </template>

            <v-card>
              <v-card-title class="text-h5 primary white--text">
                New Manager
              </v-card-title>

              <v-card-text>
                <v-form ref="formAdd">
                  <v-row>
                    <v-col cols="12" md="6">
                      <v-text-field
                        label="FirstName"
                        v-model="newManager.FirstName"
                        :rules="nameRule"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                      <v-text-field
                        label="LastName"
                        v-model="newManager.LastName"
                        :rules="nameRule"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" md="6">
                      <v-select
                        :items="genders"
                        label="Gender"
                        v-model="newManager.Gender"
                      ></v-select>
                    </v-col>
                    <v-col cols="12" md="5">
                      <v-dialog
                        ref="dialog"
                        v-model="modal"
                        :return-value.sync="date"
                        persistent
                        width="290px"
                      >
                        <template v-slot:activator="{ on, attrs }">
                          <v-text-field
                            v-model="newManager.Date_Of_Birth"
                            prepend-icon="mdi-calendar"
                            label="Date of birth"
                            v-bind="attrs"
                            v-on="on"
                          ></v-text-field>
                        </template>
                        <v-date-picker
                          v-model="newManager.Date_Of_Birth"
                          scrollable
                        >
                          <v-spacer></v-spacer>
                          <v-btn text color="primary" @click="modal = false">
                            Cancel
                          </v-btn>
                          <v-btn
                            text
                            color="primary"
                            @click="$refs.dialog.save(date)"
                          >
                            OK
                          </v-btn>
                        </v-date-picker>
                      </v-dialog>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12">
                      <v-text-field
                        v-model="newManager.Email"
                        :rules="emailRule"
                        label="Email"
                      >
                      </v-text-field>
                    </v-col>
                  </v-row>
                  <v-row>
                    <v-col cols="12" md="6">
                      <v-text-field
                        label="Password"
                        v-model="newManager.Password"
                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                        :type="show1 ? 'text' : 'password'"
                        name="input-10-1"
                        hint="At least 8 characters"
                        counter
                        @click:append="show1 = !show1"
                        :rules="passwordRule"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" md="6">
                      <v-text-field
                        label="Password"
                        v-model="newManager.ConfirmPassword"
                        :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                        :type="show2 ? 'text' : 'password'"
                        name="input-10-1"
                        hint="At least 8 characters"
                        counter
                        @click:append="show2 = !show2"
                        :rules="ConfirmpasswordRule"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-form>
              </v-card-text>

              <v-divider></v-divider>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" text @click="handleAddManager">
                  Add
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-col>
      </v-row>
    </v-container>
    <!-- Table data -->
    <v-container>
      <v-data-table
        :headers="headers"
        :items="users"
        sort-by="LastName"
        class="elevation-1"
        :search="searchingUser"
      >
        <template v-slot:top>
          <v-dialog v-model="dialogDelete" max-width="500px">
            <v-card>
              <v-card-title class="text-h5"
                >Are you sure you want to delete this user?</v-card-title
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
        </template>
        <template v-slot:[`item.actions`]="{ item }">
          <v-icon small @click="deleteItem(item)"> mdi-delete </v-icon>
        </template>
      </v-data-table>
    </v-container>
  </div>
</template>

<script>
import { END_POINTS, createApiEndPoints } from "@/api.js";
export default {
  name: "Managers",
  data() {
    return {
      searchingUser: "",
      dialogAdd: false,
      dialog: false,
      dialogDelete: false,
      headers: [
        {
          text: "FirstName",
          align: "start",
          sortable: false,
          value: "FirstName",
        },
        { text: "LastName", value: "LastName" },
        { text: "date of birth", value: "Date_Of_Birth", sortable: false },
        { text: "Height", value: "Height", sortable: false },
        { text: "Weight (g)", value: "Weight", sortable: false },
        { text: "Avatar", value: "Avatar", sortable: false },
        { text: "Actions", value: "actions", sortable: false },
      ],
      users: [
        {
          id_User: 1,
          FirstName: "Anas",
          LastName: "Mellas",
          Date_Of_Birth: "23/05/2001",
          Height: 0,
          Weight: 0,
          Avatar: "avatar",
        },
        {
          id_User: 2,
          FirstName: "Jamal",
          LastName: "Idaissa",
          Date_Of_Birth: "23/05/2001",
          Height: 0,
          Weight: 0,
          Avatar: "avatar",
        },
      ],
      editedIndex: -1,
      editedItem: {
        name: "",
        calories: 0,
        fat: 0,
        carbs: 0,
        protein: 0,
      },
      defaultItem: {
        name: "",
        calories: 0,
        fat: 0,
        carbs: 0,
        protein: 0,
      },
      newManager: {
        FirstName: "",
        LastName: "",
        Email: "",
        Password: "",
        Gender: "",
        ConfirmPassword: "",
        Date_Of_Birth: "",
        Role: "Manager",
      },
      show1: false,
      show2: false,
      genders: ["Male", "Female"],
      date: new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
        .toISOString()
        .substr(0, 10),
      modal: false,

      // input Rules
      nameRule: [
        function (name) {
          let nameRegex = new RegExp("^[a-zA-Z]+([ a-zA-Z]+)?$");
          if (!nameRegex.test(name)) {
            return "The name must contain characters only";
          }
          return true;
        },
      ],
      genderRule: [
        (gender) => gender.value != null || "Please select your gender",
      ],
      dobRule: [
        (dob) =>
          (dob.value != null && dob.value < new Date()) ||
          "Please verify the date of birth",
      ],
      emailRule: [
        function (email) {
          let emailRegex = new RegExp(
            "^[a-zA-Z0-9]+((._-)[a-zA-Z0-9]+)?@[a-zA-Z]+.(com|fr|uk|net)$"
          );
          if (!emailRegex.test(email)) {
            return "please enter a valid email adresse";
          }
          return true;
        },
      ],
      passwordRule: [
        (password) =>
          password.length >= 2 ||
          "Password should be at least 8 characters (numbers, letters and symbols)",
      ],
      ConfirmpasswordRule: [
        (password) =>
          password == this.newManager.Password || "Password not match",
      ],
    };
  },

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "New Item" : "Edit Item";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
    dialogDelete(val) {
      val || this.closeDelete();
    },
  },

  methods: {
    handleAddManager() {
      if(this.$refs.formAdd.validate()) {

        createApiEndPoints(END_POINTS.ADD_MANAGER)
          .create({...this.newManager})
          .then(response => {
            console.log(response);
            console.log(this.newManager.Email);
          })
          .catch(error => console.log(error));

      } else {
        console.log("form validation failed");
      }
      this.dialogAdd = false;
    },

    deleteItem(item) {
      this.editedIndex = this.users.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.users.splice(this.editedIndex, 1);
      this.closeDelete();
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
  },
};
</script>
