<script lang="ts" setup>
</script>

<template>
    <div class="box">
        <slot name="left-bar"></slot>
        <div class="content">
            <slot></slot>
        </div>
    </div>
</template>

<style scoped>
    
.box {
    height: 100%;
    display: flex;
    flex-direction: row;
}

.content {
    background: var(--content-background);
    height: 100%;
    width: 100%;
}

</style>