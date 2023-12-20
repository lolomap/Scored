<script lang="ts" setup>

import { Grades } from '@/pages/grades_page';
import { Auth } from '@/pages/auth_page';

import { useEntityStore } from '@/entities/store';
import router from '@/app/router';

const entityStore = useEntityStore();

if (entityStore.user.id < 0)
    router.push('/auth');
else router.push('/grades');

</script>

<template>

</template>