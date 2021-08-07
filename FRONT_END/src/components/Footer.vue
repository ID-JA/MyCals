<template>
  <footer class="primary white--text pt-5">
    <v-container>
      <v-row wrap>
        <v-col cols="12" sm="12" md="6">
          <div class="footer__icons footer__address mb-4">
            <v-icon class="white--text mr-3">place</v-icon>
            <span class="font-weight-light"
              >21 Revolution street
              <span class="font-weight-bold">Paris, France</span></span
            >
          </div>
          <div class="footer__icons footer__phone mb-4">
            <v-icon class="white--text mr-3">phone</v-icon>
            <span class="font-weight-light">+1 555 123456</span>
          </div>
          <div class="footer__icons footer__address mb-4">
            <v-icon class="white--text mr-3">email</v-icon>
            <span class="font-weight-light">support@company.com</span>
          </div>
        </v-col>
        <v-col cols="12" sm="12" md="6">
          <h2 class="font-weight-bold mb-3">About MyCals</h2>
          <p class="font-weight-light">
            simply dummy text of the printing and typesetting industry. Lorem
            Ipsum has been the industry's standard
          </p>
        </v-col>
      </v-row>
      <!-- copyright -->
      <p class="text-center mt-5">
        All rights reserved to MyCals company &copy; {{ getCurrentYear }}
      </p>
    </v-container>
  </footer>
</template>

<script>
export default {
  name: "Footer",
  computed: {
    getCurrentYear() {
      return new Date().getFullYear();
    },
  },
};
</script>
