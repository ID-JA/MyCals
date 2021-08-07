<template>
  <div class="banner">
    <v-container>
      <v-row class="d-flex align-center justify-space-between">
        <v-col cols="12" sm="12" md="5">
          <h1 class="secondary--text banner__intro">
            Protect your <span class="primary--text">health</span> by tracking
            your <span class="primary--text">calories</span>
          </h1>
          <p class="third--text mb-5">
            Get ready to join one of the biggest comunities in the field of
            health and nuitrition, start your journey now.
          </p>
          <v-btn depressed class="primary font-weight-light"> Start now </v-btn>
        </v-col>
        <v-col cols="12" sm="12" md="7">
          <div class="banner__img">
            <img
              src="../../assets/banner-img.svg"
              alt=""
              style="max-width: 100%"
              draggable="false"
            />
            <!-- <v-responsive>
            </v-responsive> -->
          </div>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "Banner",
};
</script>

<style lang="scss" scoped>
.banner {
  margin-top: 4rem;
}
.banner__intro {
  font-size: 40px;
}
</style>
