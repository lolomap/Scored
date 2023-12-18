<script lang="ts" setup>
import { Container } from '@/shared/container';
import { Typography } from '@/shared/typography';
import { type WorkModel } from '@/entities/work';
import { Field } from '@/shared/field';

import { useEntityStore } from '@/entities/store';
import { computed } from 'vue';

interface Props {
    work: WorkModel
}

const entityStore = useEntityStore();
const user = computed(() => entityStore.user);
const studentId = computed(() => entityStore.studentId);

const props = defineProps<Props>();
const {
    work = {
        id: -1,
        name: "",
        score: -1,
        max: 0
    }
} = props;

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: ''
};

const onEnterScore = (value: string) => {
    requestOptions.body = JSON.stringify(
        {
            'studentId': studentId.value,
            'workId': work.id,
            'score': +value
        }
    );

    fetch('https://localhost:7083/change_score', requestOptions)
        .then(response => response.json())
        .then(data => {});
};
const onEnterMaxScore = () => {};

</script>

<template>
    <div class="work_slot">
        <Container class="work_slot_container">
            <div class="work_name">
                <Typography tag="p">{{ work.name }}</Typography>
            </div>
            <div class="work_score">

                <Typography 
                    tag="p" 
                    v-if="(studentId > 0) && (user.type == 'student')" 
                    class="work_score_text"
                >
                    {{ work.score }}
                </Typography>
                <Field 
                    class="input_score"
                    v-if="(studentId > 0) && (user.type != 'student')"
                    :value="work.score?.toString()"
                    :onEnter="onEnterScore"
                />

                <Typography tag="p" v-if="studentId > 0" class="work_score_max_text">/</Typography>

                <Typography tag="p" class="work_score_max_text" v-if="studentId > 0">{{ work.max }}</Typography>
                <Field 
                    class="input_score"
                    v-if="studentId <= 0"
                    :value="work.max.toString()"
                />
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
}

.work_score {
    display: flex;
    width: 50%;
    gap: 8px;
}

.work_score_text {
    margin-left: auto;
}

.work_score_max_text {
    margin-left: auto;
}

.input_score {
    width: 64px;
}

</style>