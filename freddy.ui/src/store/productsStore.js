import api from "../api/FreddyApi";

export default {
  namespaced: true,
  state: {
    foo: false,
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
    },
    doFoo: (state, v) => state.foo = v
  },
  actions: {
    async loadProducts({commit}) {

      commit("startLoading");
      commit('clearErrors');
      commit('setProducts', []);

      try {
        const response = await api.get('http://localhost:5008/api/freddy/products');
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
