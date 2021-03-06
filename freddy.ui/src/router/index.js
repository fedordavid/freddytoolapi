import Vue from 'vue'
import VueRouter from 'vue-router'

const createView =
  (name, path = '', componentName = null) => ({ 
    path: `/${path}`, 
    name,
    component: () => import(`@/views/${componentName}`),
    props: path.includes(':')
  })

const routes = [
  {
    path: '/',
    component: () => import('../layouts/BaseLayout.vue'),
    children: [
      createView('Home', '', 'Home'),
      createView('Products', 'products', 'Products/ProductsList'),
      createView('Customers', 'customers', 'Customers/CustomersList'),
      createView('Orders', 'orders', 'Orders/OrdersList'),
      createView('Order', 'orders/:orderId/', 'Orders/OrderDetail'),
    ]
  }
]

Vue.use(VueRouter)

const router = new VueRouter({
  routes
})

export default router
