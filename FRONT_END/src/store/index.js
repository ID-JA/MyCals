import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentToken: "",
  },
  mutations: {
    getUserToken: (state, token) => {
      state.currentToken = token;
    },
  },
  actions: {
    getUserToken: (context, token) => {
      context.commit("getUserToken", token);
    },
  },
  modules: {},
});
