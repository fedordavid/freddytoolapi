import api from '@/api/FreddyApi';

import itemStatuses from '@/store/ordersStore/orderItemStatuses';

const statuses = {
  pending: 'Pending',
  done: 'Done'
}

const mockOrder = {
  id: 'foo',
  name: 'Foo Order',
  description: 'Lorem ipsum dolor sit amet',
  created: '25/10/2020',
  status: statuses.pending,
  customerId: '8e704345-26bc-4091-a9cc-0ca052c03556',
  customerName: 'Ilosfai-Pataki Júlia',
  orderItems: [
    { id: 'bar', name: 'Svetlomodré denim, 3 gombíky', code: 'WRUP1HA08E,J4Y', size: 'XS', qty: 3, status: itemStatuses[0] },
    { id: 'bar2', name: 'Červené denim, 1 gombík', code: 'WRUP1HA08E,J4Z', size: 'XL', qty: 7, status: itemStatuses[1] }
  ]
}

export default {
  namespaced: true,
  state: {
    isLoaded: false,
    orders: []
  },
  mutations: {
    addOrder(state, order) { state.orders = state.orders.concat(order) },
    setOrders(state, orders) { state.orders = orders },
    finishLoading(state) { state.isLoaded = true },
    orderItemDeleted(state, { orderId, orderItemIds }) { 
      const order = state.orders.find(o => o.id === orderId)
      order.orderItems = order.orderItems.filter(i => !orderItemIds.includes(i.id))
    },
    orderItemEdited(state, { orderId, orderItemId, data }) {
      const order = state.orders.find(o => o.id === orderId)
      order.orderItems = order.orderItems
          .map(i => i.id !== orderItemId ? i : {...i, qty: data.qty, status: data.status})
    }
  },
  actions: {
    async createOrder({ commit }, order){
      const { customerId } = order
      
      const orderResponse = await api.post(`customers/${customerId}/orders`);
      
      order.id = orderResponse.data.id

      commit('addOrder', order)
      
      return order.id
    },
    async loadOrders({commit}){
      //TODO: load data from API
      commit('setOrders', [mockOrder])
      commit('finishLoading')
    },
    async initializeOrders({dispatch, state}) {
      if (state.isLoaded)
        return;

      dispatch('loadOrders')
    },
    async deleteOrderItems({commit}, { orderId, orderItemIds }) {
      commit('orderItemDeleted', { orderId, orderItemIds })
    },
    async editOrderItem({commit}, { orderId, orderItemId, data }) {
      commit('orderItemEdited', { orderId, orderItemId, data })
    }
  },
  getters: {
    allOrders: state => state.orders
  }
};
