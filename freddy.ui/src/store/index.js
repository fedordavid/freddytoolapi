import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

import productsStore from "./productsStore";
import customersStore from "./customersStore";

export default new Vuex.Store({
  modules: {
    products: productsStore,
    customers: customersStore
  }
})
