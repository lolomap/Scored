<script lang="ts" setup>
import type { UserModel } from '@/entities/user';
import { Typography } from '@/shared/typography';
import { Button } from '@/shared/button';


interface Props {
    items: Array<UserModel>;
    item_action: (item: UserModel) => {}
}

const props = defineProps<Props>();
const {
    items = [],
    item_action = () => {}
} = props;
</script>

<template>
    <div v-for="item in items" class="user_list_container">
        <Button class="user_button" @click="item_action(item)">{{ item.name }}</Button>
    </div>
</template>

<style scoped>

.user_list_container {
    padding: 5px;
}

</style>