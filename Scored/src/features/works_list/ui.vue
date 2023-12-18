<script lang="ts" setup>
import { Container } from '@/shared/container/';
import { WorkSlot } from '@/features/work_slot';
import { type WorkModel } from '@/entities/work';

interface Props {
    items?: Array<WorkModel>;
}

const props = defineProps<Props>();
const {
    items = []
} = props;
</script>

<template>
    <Container class="list_container" v-for="item in items">
        <WorkSlot :work="item" />
    </Container>
</template>

<style scoped>

.list_container {
    display: flex;
    align-items: center;
    flex-direction: column;
}

</style>