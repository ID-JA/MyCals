<template>
  <div class="users">
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
    <v-container>
      <v-data-table
        :headers="headers"
        :items="users"
        sort-by="Lastname"
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
export default {
  name: "Users",
  data() {
    return {
      searchingUser: "",
      dialog: false,
      dialogDelete: false,
      headers: [
        {
          text: "Firstname",
          align: "start",
          sortable: false,
          value: "Firstname",
        },
        { text: "Lastname", value: "Lastname" },
        { text: "date of birth", value: "date_of_birth", sortable: false },
        { text: "Height", value: "Height", sortable: false },
        { text: "Weight (g)", value: "Weight", sortable: false },
        { text: "Avatar", value: "Avatar", sortable: false },
        { text: "Actions", value: "actions", sortable: false },
      ],
      users: [
        {
          id_User: 1,
          Firstname: "Anas",
          Lastname: "Mellas",
          date_of_birth: "23/05/2001",
          Height: 0,
          Weight: 0,
          Avatar: "avatar",
        },
        {
          id_User: 2,
          Firstname: "Jamal",
          Lastname: "Idaissa",
          date_of_birth: "23/05/2001",
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
