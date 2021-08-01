<template>
  <div class="banner">
    <Navbar />
    <Banner />
    <!-- Why Us Section -->
    <section class="why-us text-center">
      <v-container>
        <h4 class="text-h4">Why MyCals?</h4>
        <WhyComponent />
        <v-btn depressed class="primary font-weight-light">
          Start your journey now!
        </v-btn>
      </v-container>
    </section>
    <!-- Testimonial -->
    <section class="testimonial">
      <v-container>
        <h4 class="text-h4 testimonial__title text-center mb-4">
          Successfull stories
        </h4>
        <p class="third--text testimonial__description text-center">
          Hundreds of users have reached their goals using myCals.
        </p>
        <p class="third--text testimonial__description text-center mb-15">
          don’t forget to share your journey too in order to inspire others!
        </p>
        <TestimonialCard />
      </v-container>
    </section>
    <!-- Footer -->
    <Footer />
  </div>
</template>

<script>
import Navbar from "@/components/Navbar.vue";
import Banner from "../components/Home/Banner.vue";
import WhyComponent from "../components/Home/WhyComponent.vue";
import TestimonialCard from "../components/Home/TestimonialCard.vue";
import Footer from "@/components/Footer.vue";

export default {
  name: "home",
  components: {
    Navbar,
    Banner,
    WhyComponent,
    TestimonialCard,
    Footer,
  },
};
</script>

<style lang="scss" scoped>
.testimonial,
.why-us {
  margin: 100px 0;
}

// Why Us

// Testimonial
.testimonial {
  .testimonial__title {
    margin-bottom: 20px;
  }

  .testimonial__description:last-of-type {
    margin-bottom: 70px;
  }
}
</style>
