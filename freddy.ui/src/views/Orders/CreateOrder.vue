<template>
  <div>
    <v-btn @click="addOrderDialog = true" color="primary darken-1" bottom right fixed fab>
      <v-icon>mdi-plus</v-icon>
    </v-btn>

    <v-dialog v-model="addOrderDialog">
      <v-card class="">
        <v-card-title class="headline">Create Order</v-card-title>

        <v-card-text>
          <v-text-field v-model="orderName" label="Order Name" autofocus filled hide-details class="mt-4" />
          
          <v-combobox v-model="customer" label="Customer Name" filled class="mt-4" 
                      hint="Select customer or create a new one" persistent-hint
                      :items="customerItems" :loading="isLoading" item-value="id" item-text="name" />
          
          <template v-if="shouldAddCustomer">
            <v-text-field v-model="customerEmail" label="Customer Email" filled hide-details class="mt-4" />
            <v-text-field v-model="customerPhone" label="Customer Phone" filled hide-details class="mt-4" />
            <v-alert icon="mdi-alert-circle-outline" text color="primary" class="mt-4 mb-0">
              A new customer will be created
            </v-alert>
          </template>
          
        </v-card-text>

        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn @click="addOrderDialog = false" color="primary darken-1" text>
            Cancel
          </v-btn>

          <v-btn @click="submit" color="primary darken-1" text :disabled="shouldAddCustomer">
            Create
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>  
</template>

<script>
import router from "@/router";

import { createNamespacedHelpers } from 'vuex'

const { mapActions: mapCustomersActions, mapGetters: mapCustomersGetters, mapState: mapCustomersState } = createNamespacedHelpers('customers')
const { mapActions: mapOrderActions } = createNamespacedHelpers('orders')

export default {
  name: 'CreateOrder',
  data() {
    return {
      addOrderDialog: false,
      orderName: '',
      customerEmail: '',
      customerPhone: '',
      customer: undefined,
    }
  },
  created() {
    this.initializeCustomers()
  },
  computed: {
    shouldAddCustomer() { return this.customer && typeof this.customer === 'string' },
    ...mapCustomersGetters(['customerItems']),
    ...mapCustomersState(['hasLoadingErrors', 'isLoading'])
  },
  methods: {
    async submit() {
      const orderId = await this.createOrder({ customerId: this.customer.id, name: this.orderName })
      await router.push({ name: 'Order', params: { orderId: orderId } })
    },
    ...mapCustomersActions(['initializeCustomers']),
    ...mapOrderActions(['createOrder'])
  }
}
</script>