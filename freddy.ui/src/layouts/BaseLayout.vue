<template>
  <v-app>
    <v-app-bar app clipped-left>
      <v-toolbar-title>Freddy</v-toolbar-title>
    </v-app-bar>
    <v-navigation-drawer app clipped>
      <v-list dense >
        <navigation-drawer-item to="Home" title="Home" icon="mdi-home" />
        <navigation-drawer-item to="Products" title="Products" icon="mdi-tshirt-crew" />
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
    components: { NavigationDrawerItem }
  }
</script>

<style scoped>

</style>