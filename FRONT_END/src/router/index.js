import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import About from "../views/About.vue";
import Login from "../views/Login.vue";
import Signup from "../views/Signup.vue";
import UserDashboard from "../views/User/UserDashboard.vue";
import Meals from "../views/User/Meals.vue";
import UserStats from "../views/User/UserStats.vue";
import managerDashboard from "../views/Manager/managerDashboard.vue";


Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/signup",
    name: "Signup",
    component: Signup,
  },
  {
    path: "/UserDashboard",
    name: "UserDashboard",
    component: UserDashboard,
    children: [
      {
        path: "Meals",
        component: Meals,
      },
      {
        path: "UserStats",
        component: UserStats,
      }
    ]
  },
  {
    path: "/managerDashboard",
    name: "managerDashboard",
    component: managerDashboard,
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
