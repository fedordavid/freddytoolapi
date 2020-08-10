<template>
  <v-container>
    <v-data-table :headers="headers" :items="customerItems" item-key="id" :loading="isLoading" />
    <v-snackbar v-model="hasLoadingErrors" color="error">
      An Error occured during loading of the data...
    </v-snackbar>
  </v-container>
</template>

<script>

import { createNamespacedHelpers } from 'vuex'
const { mapActions, mapGetters, mapState } = createNamespacedHelpers('customers')

export default {
  data() {
    return {
      headers: [
        { text: 'Name', value: 'name'},
        { text: 'Email', value: 'email'},
        { text: 'Phone', value: 'phone'}
      ]
    }
  },
  created() {
    this.initializeCustomers();
  },
  computed: {
    ...mapGetters(['customerItems']),
    ...mapState(['hasLoadingErrors', 'isLoading', 'foo'])
  },
  methods: {
    ...mapActions(['initializeCustomers'])
  }
}
</script>
