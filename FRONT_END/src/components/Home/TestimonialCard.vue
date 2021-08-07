<template>
  <div class="testimonial-card">
    <carousel
      :autoplay="false"
      :nav="false"
      :responsive="{
        0: { items: 1, nav: false },
        600: { items: 1, nav: false },
      }"
    >
      <v-card outlined max-width="600" class="mx-auto testimonial__card">
        <v-row wrap class="align-items-stretch">
          <v-col cols="12" sm="12" md="4">
            <img
              src="https://source.unsplash.com/random"
              alt=""
              class="testimonial__img"
            />
            <!-- <v-responsive>
            </v-responsive> -->
          </v-col>
          <v-col cols="12" sm="12" md="8" class="ikhan">
            <v-card-title class="font-weight-bold">
              From fast-food addict to mindful eater
            </v-card-title>
            <v-card-text>
              <p>
                “I naturally fell into the concept of mindful eating… it takes
                15-20 minutes for the food to hit your stomach, so I started
                eating slower.”
              </p>
              <h3 class="primary--text font-weight-bold">Anna Smith</h3>
              <v-rating
                v-model="rating"
                color="primary"
                hover
                length="5"
                readonly
                size="25"
              ></v-rating>
            </v-card-text>
          </v-col>
        </v-row>
      </v-card>
    </carousel>
  </div>
</template>

<script>
import carousel from "vue-owl-carousel";
export default {
  name: "TestimonialCard",
  data() {
    return {
      rating: 4,
    };
  },
  components: {
    carousel,
  },
};
</script>

<style lang="scss" scoped>
.testimonial__card {
  position: relative;

  .ikhan:after {
    contain: "";
    position: absolute;
    top: 0;
    right: 0;
    width: 10px;
    height: 10px;
    background-color: red;
  }
}
.testimonial__img {
  height: 240px;
  object-fit: contain;
}
</style>
