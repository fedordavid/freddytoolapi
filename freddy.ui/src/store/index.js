import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

import productsStore from "./productsStore";
import customersStore from "./customersStore";
import ordersStore from "./ordersStore";

export default new Vuex.Store({
  modules: {
    products: productsStore,
    customers: customersStore,
    orders: ordersStore
  }
})
