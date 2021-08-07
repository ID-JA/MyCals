<template>
  <nav class="mb-5">
    <v-app-bar class="white" app elevate-on-scroll>
      <v-container class="d-flex align-center justify-space-between">
        <div class="nav__logo">
          <span class="text-h5">My</span>
          <span class="primary--text text-h5">Cals</span>
        </div>
        <div class="banner__humburger" @click="toggleNavbar">
          <span class="line"></span>
          <span class="line"></span>
          <span class="line"></span>
        </div>
        <div class="nav__links">
          <router-link to="/" exact>home</router-link>
          <router-link to="/About">about us</router-link>
          <router-link to="/Login">log in</router-link>
          <router-link to="/Signup">sign up</router-link>
        </div>
      </v-container>
    </v-app-bar>
  </nav>
</template>

<script>
export default {
  name: "Navbar",
  methods: {
    toggleNavbar() {
      let navbar = document.querySelector(".nav__links");
      let lines = document.querySelectorAll(".line");

      if (this.collapsed) {
        lines[0].setAttribute(
          "style",
          "width: 30px;height: 5px; background: #fff;margin: 5px; transition: all 0.3s ease-in-out;"
        );
        lines[1].setAttribute(
          "style",
          "width: 30px;height: 5px; background: #fff;margin: 5px; transition: all 0.3s ease-in-out;"
        );
        lines[2].setAttribute(
          "style",
          "width: 30px;height: 5px; background: #fff;margin: 5px; transition: all 0.3s ease-in-out;"
        );
        navbar.style.bottom = "100%";

        this.collapsed = false;
      } else {
        lines[0].style.transform = "translateY(10px) rotate(225deg)";
        lines[1].setAttribute(
          "style",
          "opacity:0; -moz-opacity:0; filter:alpha(opacity=0)"
        );
        lines[2].style.transform = "translateY(-10px) rotate(-225deg)";

        navbar.style.bottom = "0";
        this.collapsed = true;
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "../scss/variables.scss";

a.router-link-exact-active {
  color: $purple-color !important;
}

.nav__links > a {
  margin: 10px;
}

// navbar toggle
.banner__humburger {
  display: none;
  // position: fixed;
  // right: 2%;
  // top: 3%;
  // z-index: 99;
  cursor: pointer;

  .line {
    display: block;
    width: 30px;
    height: 2px;
    background: $black-color;
    margin: 5px;
    transition: all 0.3s ease-in-out;
  }
}

@media screen and (max-width: 767px) {
  .banner__humburger {
    display: block !important;
    position: fixed;
    right: 30px;
    z-index: 10;
  }

  .nav__links {
    // display: none !important;
    // background: linear-gradient(rgba(0, 0, 0, 0.7), rgba(0, 0, 0, 0.7), #252837);
    background-color: $purple-color;
    position: fixed;
    top: 0%;
    left: 0;
    right: 0;
    bottom: 100%;
    width: auto;
    height: auto;
    z-index: 3;
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    text-align: center;
    overflow: hidden;
    transition: all 0.5s ease-in-out;
    margin-top: 0 !important;
  }
}
</style>
