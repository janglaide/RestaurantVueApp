<template>
  <div class="container">
    <div
      v-for="dish in state.dishes"
      :key="dish.id"
      class="card">
      <div>
        <h4>{{ dish.name }}</h4>
        <h5>{{ dish.portionName }}</h5>
        <a>{{ dish.price }}</a>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { reactive } from "vue";

  const DISH_API = "https://localhost:5001/api/dish";

  const state = reactive({
    dishes: Object,
  });

  fetch(DISH_API)
    .then((response) => response.json())
    .then((t) => (state.dishes = t));
</script>

<style scoped>
  .container {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
  }

  .card {
    margin: 20px;
    background-color: aliceblue;
    padding: 20px;
    width: 200px;
    height: auto;
    border-radius: 5%;
  }

  .card a {
    font-weight: 1000;
  }
</style>
