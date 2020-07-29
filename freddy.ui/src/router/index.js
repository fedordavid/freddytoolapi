import Vue from 'vue'
import VueRouter from 'vue-router'

const createView =
  (name, path = '', componentName = null) => ({ path: `/${path}`, name, component: () => import(`../views/${componentName}.vue`)})

const routes = [
  {
    path: '/',
    component: () => import('../layouts/BaseLayout.vue'),
    children: [
      createView("Home", "", "Home"),
      createView("Products", "Products", "Products/ProductsList")
    ]
  }
]

Vue.use(VueRouter)

const router = new VueRouter({
  routes
})

export default router
