<template>
  <v-app>
    <v-app-bar app clipped-left>
      <v-app-bar-nav-icon @click="isNavigationShown = !isNavigationShown" />
      <v-toolbar-title>Freddy</v-toolbar-title>
    </v-app-bar>
    <v-navigation-drawer app clipped v-model="isNavigationShown">
      <v-list dense >
        <navigation-drawer-item to="Home" title="Home" icon="mdi-home" />
        <navigation-drawer-item to="Products" title="Products" icon="mdi-tshirt-crew" />
        <navigation-drawer-item to="Customers" title="Customers" icon="mdi-human-male-female" />
        <navigation-drawer-item to="Orders" title="Orders" icon="mdi-package-variant" />
      </v-list>
    </v-navigation-drawer>
    <v-main app>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
  import NavigationDrawerItem from "../components/NavigationDrawerItem";
  export default {
    name: "BaseLayout",
    data() {
      return {
        isNavigationShown: undefined
      }
    },
    components: { NavigationDrawerItem }
  }
</script>

<style scoped>

</style>