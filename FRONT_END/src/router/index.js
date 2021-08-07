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
import AdminDashboard from "../views/Admin/AdminDashboard.vue";
import AppStats from "../views/Admin/AppStats.vue";
import Users from "../views/Admin/Users.vue";
import Managers from "../views/Admin/Managers.vue";

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
        name: "UserDashboardMeals",
        path: "Meals",
        component: Meals,
      },
      {
        name: "UserDashboardStats",
        path: "UserStats",
        component: UserStats,
      },
    ],
  },
  {
    path: "/managerDashboard",
    name: "managerDashboard",
    component: managerDashboard,
  },
  {
    path: "/AdminDashboard",
    name: "AdminDashboard",
    component: AdminDashboard,
    children: [
      {
        name: "AdminDashboardAppStats",
        path: "AppStats",
        component: AppStats,
      },
      {
        name: "AdminDashboardUsers",
        path: "Users",
        component: Users,
      },
      {
        name: "AdminDashboardManagers",
        path: "Managers",
        component: Managers,
      },
    ],
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
