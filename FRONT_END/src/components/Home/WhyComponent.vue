<template>
  <div class="why-component">
    <v-container>
      <v-row wrap>
        <v-col v-for="why in whys" :key="why.title" cols="12" sm="12" md="4">
          <div id="hexagon" class="primary white--text">
            <v-icon class="white--text" large>{{ why.icon }}</v-icon>
          </div>
          <h4 class="text-h4 mb-3">{{ why.title }}</h4>
          <p class="third--text">{{ why.description }}</p>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script>
export default {
  name: "WhyComponent",
  data() {
    return {
      whys: [
        {
          icon: "school",
          title: "Learn",
          description:
            "Learn about the essential of diets and nuitrition and how it keeps you healthy and in a good shape",
        },
        {
          icon: "gps_fixed",
          title: "Track",
          description: "Track your calories and nuitrition very easily",
        },
        {
          icon: "trending_up",
          title: "Improve",
          description:
            "Get your body to be in shape and healthy and increase your likelihood of hitting your goals.",
        },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
.why-component {
  margin: 60px 0;
}

#hexagon {
  width: 100px;
  height: 57.735px;
  background: #6c63ff;
  position: relative;
  margin: auto;
  margin-bottom: 40px;
  display: flex;
  align-items: center;
  justify-content: center;
}
#hexagon::before {
  content: "";
  position: absolute;
  top: -28.8675px;
  left: 0;
  width: 0;
  height: 0;
  border-left: 50px solid transparent;
  border-right: 50px solid transparent;
  border-bottom: 28.8675px solid #6c63ff;
}
#hexagon::after {
  content: "";
  position: absolute;
  bottom: -28.8675px;
  left: 0;
  width: 0;
  height: 0;
  border-left: 50px solid transparent;
  border-right: 50px solid transparent;
  border-top: 28.8675px solid #6c63ff;
}
</style>
