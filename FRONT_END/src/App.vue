<template>
  <v-app>
    <navbar />

    <v-main>
      <router-view />
    </v-main>
    
  </v-app>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
export default {
  name: "App",
  components: {
    Navbar,
  },

  data: () => ({
    //
  }),
};
</script>

<style lang="scss"></style>
