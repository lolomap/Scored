<script lang="ts" setup>
</script>

<template>
    <div class="left_bar">
        <slot></slot>
    </div>
</template>

<style scoped>

.left_bar {
    background: var(--content-left-background);
    width: 15%;
    padding: 15px;
}

</style>