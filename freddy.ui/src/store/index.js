import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

import productsStore from "./productsStore";

export default new Vuex.Store({
  modules: {
    products: productsStore
  }
})
