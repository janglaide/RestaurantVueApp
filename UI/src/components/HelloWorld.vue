<template>
    <div class="post">
        <div>
            Hello world
            <span>{{ state.message }} !</span>
        </div>
    
    </div>
</template>

<script setup>

import {defineProps, reactive} from 'vue'

defineProps({
    msg: String,
})

const state = reactive({
    count:0,
    message: 'empty'
})

fetch('https://localhost:5001/api/home')
    .then(response => response.text())
    .then(t => state.message = t)
    
</script>