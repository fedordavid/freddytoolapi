<template>
  <v-container>
    <v-data-table :headers="headers" :items="allOrders" item-key="id" :loading="isLoading"
                  @click:row="detail"/>
    <v-snackbar v-model="hasLoadingErrors" color="error">
      An Error occured during loading of the data...
    </v-snackbar>

    <CreateOrder />

  </v-container>
</template>

<script>

import { createNamespacedHelpers } from 'vuex'
const { mapGetters, mapActions } = createNamespacedHelpers('orders')

import CreateOrder from "@/views/Orders/CreateOrder";
import router from "@/router";

export default {
  name: 'OrdersList',
  data() {
    return {
      headers: [
        { text: 'Name', value: 'name'},
        { text: 'Customer Name', value: 'customerName'},
        { text: 'Created', value: 'created'}
      ]
    }
  },
  created() {
    this.initializeOrders();
  },
  computed: {
    isLoading: () => false,
    hasLoadingErrors: () => false,
    ...mapGetters(['allOrders'])
  },
  methods: {
    async detail(_, { item: { id } }) {
      await router.push({ name: 'Order', params: { orderId: id } })
    },
    ...mapActions(['initializeOrders'])
  },
  components: {
    CreateOrder
  }
}
</script>
