<template>
  <v-container>
    <v-data-table :headers="headers" :items="productItems" item-key="id" :loading="isLoading" />
    <v-snackbar v-model="hasLoadingErrors" color="error">
      An Error occured during loading of the data...
    </v-snackbar>
  </v-container>
</template>

<script>

import { createNamespacedHelpers } from 'vuex'
const { mapActions, mapGetters, mapState } = createNamespacedHelpers('products')

export default {
  data() {
    return {
      headers: [
        { text: 'Name', value: 'name'},
        { text: 'Code', value: 'code'},
        { text: 'Size', value: 'size'}
      ]
    }
  },
  created() {
    this.initializeProducts();
  },
  computed: {
    ...mapGetters(['productItems']),
    ...mapState(['hasLoadingErrors', 'isLoading', 'foo'])
  },
  methods: {
    ...mapActions(['initializeProducts'])
  }
}
</script>
