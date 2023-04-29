<template>
    <div v-for="dish in state.dishes" :key="dish.id">
        <div class="card">
            <h4>{{ dish.name }}</h4>
            <h5>{{ dish.portionName }}</h5>
            <a>{{ dish.price }}</a>
        </div>
    </div> 
</template>

<script setup>
import {reactive} from 'vue'

const DISH_API = "https://localhost:5001/api/dish";

const state = reactive({
    dishes: Object,
});

fetch(DISH_API)
    .then(response => response.json())
    .then(t => state.dishes = t);

</script>

<style>
.card{
    margin: 20px;
    background-color: aliceblue;
}

.card h4{
    font-weight: 100;
    font-size: medium;
    color: darkmagenta;
}

.card h5{
    color: gray;
    font-size: small;
    font-family: italic;
}
</style>