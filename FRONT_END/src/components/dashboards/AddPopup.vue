<template>
  <div class="popup">
    <v-dialog v-model="dialog" width="500">
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="primary" dark v-bind="attrs" v-on="on">
          Add new meal
        </v-btn>
      </template>

      <v-card>
        <v-card-title class="text-h5 grey lighten-2">
          Add new meal
        </v-card-title>

        <v-card-text
          >id, date, time, text, numCals
          <v-form ref="form">
            <v-text-field
              label="description"
              v-model="description"
              prepend-icon="folder"
              :rules="descriptionRules"
            ></v-text-field>
            <v-textarea
              label="number of calories"
              v-model="numCals"
              prepend-icon="number"
              :rules="numCalsRules"
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
                  prepend-icon="mdi-calendar"
                  v-bind="attrs"
                  @blur="date = parseDate(dateFormatted)"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="date"
                no-title
                @input="menu1 = false"
              ></v-date-picker>
            </v-menu>
            <v-time-picker
              ampm-in-title
              format="24hr"
              scrollable
            ></v-time-picker>
          </v-form>
        </v-card-text>

        <v-divider></v-divider>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="primary"
            text
            :loading="loading"
            @click="submit"
            class="text-lowercase"
          >
            Add
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
export default {
  name: "AddPopup",
};
</script>
