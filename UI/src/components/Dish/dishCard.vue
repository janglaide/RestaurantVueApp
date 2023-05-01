<template>
  <div>
    <h4>{{ props.vm?.name }}</h4>
    <h5>{{ props.vm?.portionName }}</h5>
    <a>{{ props.vm?.price }}</a>
  </div>
</template>

<script setup>
  import { defineProps } from "vue";

  const props = defineProps({
    vm: {
      type: Object,
      default: null,
    },
  });
</script>
