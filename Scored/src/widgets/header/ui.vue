<script lang="ts" setup>
import { useEntityStore } from '@/entities/store';
import { Container } from '@/shared/container/';
import { Typography } from '@/shared/typography';
import { computed } from 'vue';

const entityStore = useEntityStore();
const user = computed(() => entityStore.user);

</script>

<template>
    <header class="header">
        <Container class="header_container">
            <Typography tag="h4" class="user_name">{{ user.name }}</Typography>
        </Container>
    </header>
</template>

<style scoped>
.header_container {
    display: flex;
    align-items: center;
    height: 72px;
    padding: 32px;
}

.header {
    position: sticky;
    top: 0;
    left: 0;
    background: var(--header-surface);
    box-shadow: var(--shadow-default-s);
}

.user_name {
    color: var(--main-on-default);
    margin-left: auto;
}
</style>