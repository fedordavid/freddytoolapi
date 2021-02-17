import api from "../api/FreddyApi";

export default {
  namespaced: true,
  state: {
    isLoading: false,
    isLoaded: false,
    hasLoadingErrors: false,
    products: []
  },
  mutations: {
    setProducts: (state, products) => state.products = products,
    setErrors: state => state.hasLoadingErrors = true,
    clearErrors: state => state.hasLoadingErrors = false,
    startLoading: state => state.isLoading = true,
    finishLoading: state => {
      state.isLoading = false;
      state.isLoaded = true;
    }
  },
  actions: {
    async loadProducts({commit}) {

      commit("startLoading");
      commit('clearErrors');
      commit('setProducts', []);

      try {
        const response = await api.get('http://'+process.env.VUE_APP_API_URL+'/products');
        commit('setProducts', response.data);
      } catch {
        commit('setErrors');
      } finally {
        commit('finishLoading');
      }
    },

    async initializeProducts({dispatch, state}) {
      if (state.isLoaded)
        return;

      dispatch('loadProducts')
    }
  },
  getters: {
    productItems: state => state.products
  }
};
