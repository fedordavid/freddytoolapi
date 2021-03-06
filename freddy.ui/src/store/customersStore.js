import api from "../api/FreddyApi";

export default {
  namespaced: true,
  state: {
    isLoading: false,
    isLoaded: false,
    hasLoadingErrors: false,
    customers: []
  },
  mutations: {
    setCustomers: (state, customers) => state.customers = customers,
    setErrors: state => state.hasLoadingErrors = true,
    clearErrors: state => state.hasLoadingErrors = false,
    startLoading: state => state.isLoading = true,
    finishLoading: state => {
      state.isLoading = false;
      state.isLoaded = true;
    }
  },
  actions: {
    async loadCustomers({commit}) {

      commit("startLoading");
      commit('clearErrors');
      commit('setCustomers', []);

      try {
        const response = await api.get('http://'+process.env.VUE_APP_API_URL+'/customers');
        commit('setCustomers', response.data);
      } catch {
        commit('setErrors');
      } finally {
        commit('finishLoading');
      }
    },

    async initializeCustomers({dispatch, state}) {
      if (state.isLoaded)
        return;

      dispatch('loadCustomers')
    }
  },
  getters: {
    customerItems: state => state.customers
  }
};
