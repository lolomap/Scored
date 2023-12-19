<script lang="ts" setup>
import { Container } from '@/shared/container/';
import { DisciplineSlot } from '@/features/discipline_slot';
import { type DisciplineModel } from '@/entities/discipline';

interface Props {
    items?: Array<DisciplineModel>;
    noStudent?: boolean;
}

const props = defineProps<Props>();
const {
    items = [],
    noStudent = false
} = props;
</script>

<template>
    <Container class="list_container" v-for="item in items">
            <DisciplineSlot :discipline="item" :noStudent="noStudent" />
    </Container>
</template>

<style scoped>

.list_container {
    display: flex;
    align-items: center;
    flex-direction: column;
}

</style>