<script lang="ts" setup>
import { Container } from '@/shared/container';
import { Typography } from '@/shared/typography';
import { type WorkModel } from '@/entities/work';
import { Field } from '@/shared/field';
import { Button } from '@/shared/button';

import { useEntityStore } from '@/entities/store';
import { computed } from 'vue';
import router from '@/app/router';

interface Props {
    work?: WorkModel
    newWork?: boolean
}

const entityStore = useEntityStore();
const user = computed(() => entityStore.user);
const studentId = computed(() => entityStore.studentId);
const disciplineId = computed(() => entityStore.disciplineId);

const props = defineProps<Props>();
const {
    work = {
        id: -1,
        name: "",
        score: -1,
        max: 0
    },
    newWork = false
} = props;

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: ''
};

let score_input = '';
let name_input = '';
let max_score_input = '';
const onScoreChanged = (value: string) => score_input = value;
const onNameChanged = (value: string) => name_input = value;
const onMaxScoreChanged = (value: string) => max_score_input = value;

const onSaveWork = () => {
    if (studentId.value > 0)
    {
        requestOptions.body = JSON.stringify(
            {
                'studentId': studentId.value,
                'workId': work.id,
                'score': +score_input
            }
        );

        fetch('https://localhost:7083/change_score', requestOptions)
            .then(response => response.json())
            .then(data => {});
    }
    else if (!newWork)
    {
        requestOptions.body = JSON.stringify(
            {
                'workId': work.id,
                'name': name_input,
                'maxScore': +max_score_input
            }
        );

        fetch('https://localhost:7083/change_work', requestOptions)
            .then(response => response.json())
            .then(data => {});
    }
    else
    {
        requestOptions.body = JSON.stringify(
            {
                'disciplineId': disciplineId.value,
                'name': name_input,
                'maxScore': +max_score_input
            }
        );

        fetch('https://localhost:7083/add_work', requestOptions)
            .then(response => response.json())
            .then(data => {router.go(0)});
    }
};

</script>

<template>
    <div class="work_slot">
        <Container class="work_slot_container">
            <div class="work_name">
                <Typography tag="p" v-if="studentId > 0">{{ work.name }}</Typography>
                <Field 
                    v-if="studentId <= 0" 
                    :value="work.name"
                    :onChange="onNameChanged"
                />
            </div>
            <div class="work_right">

                <Typography 
                    tag="p" 
                    v-if="(studentId > 0) && (user.type == 'student')" 
                    class="work_score"
                >
                    {{ work.score }}
                </Typography>
                <Field 
                    class="input_score"
                    v-if="(studentId > 0) && (user.type != 'student')"
                    :value="work.score?.toString()"
                    :onChange="onScoreChanged"
                />

                <Typography tag="p" v-if="studentId > 0" class="work_score_max">/</Typography>

                <Typography tag="p" class="work_score_max" v-if="studentId > 0">{{ work.max }}</Typography>
                <Field 
                    class="input_score"
                    v-if="studentId <= 0"
                    :value="work.max.toString()"
                    :onChange="onMaxScoreChanged"
                />

                <Button
                    v-if="user.type == 'teacher'"
                    class="save_button"
                    color="good"
                    @click="onSaveWork"
                >
                    Сохранить
                </Button>
            </div>
        </Container>
    </div>
</template>

<style scoped>
.work_slot {
    width: 100%;
    padding: 10px 10px;
}

.work_slot_container {
    background: var(--slot-background);
    border-radius: 4px;
    height: 90px;
    display: flex;
    align-items: center;
    padding: 20px;
    color: var(--main-on-default)
}


.work_name {
    width: 50%;
    padding: 10px;
}

.work_right {
    display: flex;
    width: 50%;
    gap: 8px;
}

.work_score {
    margin-left: auto;
}

.work_score_max {
    margin-left: auto;
}

.input_score {
    width: 64px;
    margin-left: auto;
}

.save_button {
    margin-left: auto;
    width: auto;
}

</style>