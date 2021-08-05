import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentUser: {},
  },
  mutations: {
    setToken: (state, payload) => {
      state.currentUser = {...payload};
    }
  },
  actions: {},
  modules: {},
});
