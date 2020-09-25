<template>
  <v-container>
    <v-card-actions>
      <h3> {{orderName}} </h3>
      <span class="ml-2"> ({{customerName}}) </span>
      <v-spacer></v-spacer>
      <v-btn text>
        <v-icon left>mdi-pencil</v-icon> Edit
      </v-btn>

      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item>
            <v-list-item-title>Cancel order</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>      
      
    </v-card-actions>

    <v-data-table :headers="headers" :items="orderItems" item-key="id" 
                  @click:row="(_, {item}) => startEditItem(item)" v-model="selected"
                  show-select>
      <template v-slot:top>
        <v-toolbar v-if="!!selected.length" flat dense>
          <v-toolbar-title>Selected {{selected.length}} items</v-toolbar-title>
          <v-spacer />
          <v-btn @click="startDeleteItems(selected)" text color="error">Delete</v-btn>
        </v-toolbar>
        <v-toolbar v-else flat dense>
          <v-toolbar-title >Order Items</v-toolbar-title>
        </v-toolbar>
      </template>
    </v-data-table>

    <div style="margin-top: 72px"></div>
    
    <v-btn @click="isAddingItem = true" color="primary darken-1" bottom right fixed fab >
      <v-icon>mdi-plus</v-icon>
    </v-btn>
    
    <v-dialog v-model="isEditingItem">
      <v-card>
        <v-card-title class="headline">Editing {{itemToEdit.name}}</v-card-title>

        <v-card-text>
          <v-text-field v-model="itemToEdit.qty" label="Qty" filled/>
          <v-select v-model="itemToEdit.status" label="Status" :items="itemStatuses" return-object filled/>
        </v-card-text>

        <v-card-actions>
          <v-spacer />
          <v-btn @click="isEditingItem = false" text> Cancel </v-btn>
          <v-btn @click="editItem" text color="primary darken-1"> Save </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <v-dialog v-model="isDeletingItem">
      <v-card>
        <v-card-title class="headline"></v-card-title>

        <v-card-text>
          Are you sure you want to remove {{ itemsToDelete.length }} items
        </v-card-text>
        
        <v-card-actions>
          <v-spacer />
          <v-btn @click="isDeletingItem = false" text> Cancel </v-btn>
          <v-btn @click="deleteItem" text color="error"> Delete </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>

import { createNamespacedHelpers } from 'vuex'
import itemStatuses from '@/store/ordersStore/orderItemStatuses';

const { mapActions, mapGetters } = createNamespacedHelpers('orders')

export default {
  name: 'OrderItem',
  props: {
    orderId: String
  },
  data() {
    return {
      selected: [],
      isAddingItem: false,
      isEditingItem: false,
      isDeletingItem: false,
      itemsToDelete: [],
      itemToEdit: {},
      inEditMode: false,
      headers: [
        { text: 'Name', value: 'name', sortable: false},
        { text: 'Code', value: 'code', sortable: false},
        { text: 'Size', value: 'size', sortable: false},
        { text: 'Qty', value: 'qty', sortable: false},
        { text: 'Status', value: 'status.text', sortable: false},
      ],
      itemStatuses
    }
  },
  created() {
    this.initializeOrders();
  },
  computed: {
    currentOrder() { return this.allOrders.find(o => o.id === this.orderId) || {}},
    orderName: { get() { return this.currentOrder.name }, set() { }},
    orderDescription: { get() { return this.currentOrder.description }, set() { }},
    orderCreated: { get() { return this.currentOrder.created }, set() { }},
    orderStatus: { get() { return this.currentOrder.status }, set() { }},
    customerName() { return this.currentOrder.customerName },
    orderItems() { return this.currentOrder.orderItems },
    ...mapGetters(['allOrders'])
  },
  methods: {
    save() { this.inEditMode = false },
    cancel() { this.inEditMode = false },
    startEditItem(item) {
      this.isEditingItem = true
      this.itemToEdit = {...item}
    },
    startDeleteItems(items) {
      this.isDeletingItem = true
      this.itemsToDelete = items
    },
    deleteItem() {
      this.deleteOrderItems({ 
        orderId: this.orderId, 
        orderItemIds: this.itemsToDelete.map(i => i.id) 
      })
      this.isDeletingItem = false
      this.selected = []
    },
    editItem() {
      this.editOrderItem({
        orderId: this.orderId,
        orderItemId: this.itemToEdit.id,
        data: {
          qty: this.itemToEdit.qty,
          status: this.itemToEdit.status,
        } 
      })
      this.isEditingItem = false
    },
    ...mapActions(['initializeOrders', 'deleteOrderItems', 'editOrderItem'])
  },
  components: {

  }
}
</script>
