<script lang="ts" setup>
import { useEntityStore } from '@/entities/store';
import { Container } from '@/shared/container/';
import { Typography } from '@/shared/typography';
import { Button } from '@/shared/button';
import { computed } from 'vue';
import router from '@/app/router';

const entityStore = useEntityStore();
const user = computed(() => entityStore.user);

const onTeacherDisciplines = () => {
    entityStore.setStudentId(-1);
    router.go(0);
};

</script>

<template>
    <header class="header">
        <Container class="header_container">
            <div class="teacher_disciplines_navigate" v-if="user.type == 'teacher'">
                <Button color="normal" @click="onTeacherDisciplines">Мои дисциплины</Button>
            </div>
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