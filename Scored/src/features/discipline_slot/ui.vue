<script lang="ts" setup>
import { Container } from '@/shared/container/';
import { Typography } from '@/shared/typography/';
import { Button } from '@/shared/button';
import { type DisciplineModel } from '@/entities/discipline';

import { useEntityStore } from '@/entities';
import router from '@/app/router';

const entityStore = useEntityStore();

interface Props {
    discipline: DisciplineModel;
    noStudent?: boolean;
}

const props = defineProps<Props>();
const {
    discipline = {
        id: 0,
        name: "",
        score: 0,
        mark: 0
    },
    noStudent = true
} = props;

const onClick = () => {
    entityStore.setDisciplineId(discipline.id);
    router.push('/works')
};


let score_type = 0;
const color_types: Array<'default' | 'alert' | 'normal' | 'good' | 'great'> = ['default', 'alert', 'normal', 'good', 'great'];
const mark_types = ['Неуд.', 'Удовл-но', 'Хорошо', 'Отлично'];

if (discipline.score < 60)
    score_type = 1;
else if (discipline.score < 71)
    score_type = 2;
else if (discipline.score < 85)
    score_type = 3;
else score_type = 4;
</script>

<template>
    <div class="discipline_slot">
        <div class="discipline_slot_navigation"
            @click="onClick"
        >
            <Container class="discipline_slot_container">
                <div class="discipline_name">
                    <Typography tag="p">{{ discipline.name }}</Typography>
                </div>
                <div class="discipline_score" v-if="!noStudent">
                    <Button
                        class="discipline_score_button"
                        :color="color_types[score_type]"
                    >
                        {{ discipline.score }}
                    </Button>
                    <Button
                        class="discipline_mark_button"
                        :color="color_types[discipline.mark-1]"
                    >
                        {{ mark_types[discipline.mark-2] }}
                    </Button>
                </div>
            </Container>
        </div>
    </div>
</template>

<style scoped>

.discipline_slot {
    width: 100%;
    padding: 10px 10px;
}

.discipline_slot_container {
    background: var(--slot-background);
    border-radius: 4px;
    height: 90px;
    display: flex;
    align-items: center;
    padding: 20px;
    color: var(--main-on-default)
}

.discipline_slot_navigation {
    text-decoration: none;
}

.discipline_name {
    width: 50%;
}

.discipline_score {
    display: flex;
    width: 50%;
    gap: 8px;
}

.discipline_score_button {
    width: 42px;
    height: 42px;
    margin-left: auto;
}

.discipline_mark_button {
    width: 120px;
    height: 42px;
}

</style>