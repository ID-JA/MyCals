import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    currentUserCredentials: {},
  },
  mutations: {
    getUserCredentials: (state, credentials) => {
      state.currentUserCredentials = {...credentials};
    }
  },
  actions: {
    getUserCredentials: (context, credentials) => {
      context.commit('getUserCredentials', {...credentials});
    }
  },
  modules: {},
});
