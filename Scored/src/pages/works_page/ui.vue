<script setup lang="ts">
import { Header } from '@/widgets/header';
import { Content } from '@/widgets/content/';
import { WorksList } from '@/features/works_list';
import { WorkSlot } from '@/features/work_slot';
import { Typography } from '@/shared/typography';

import { type WorkModel} from '@/entities/work';
import { useEntityStore } from '@/entities';

import { computed, reactive } from 'vue';

const entityStore = useEntityStore();
const studentId = computed(() => entityStore.studentId);
const disciplineId = computed(() => entityStore.disciplineId);

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify(
        {
            "disciplineId": disciplineId.value,
            "studentId": studentId.value,
        }
    )
};

let works = reactive(Array<WorkModel>());
if (disciplineId.value > 0)
{
    fetch('https://localhost:7083/works', requestOptions)
        .then(response => response.json())
        .then(data => {
            data["rows"].forEach((element: { [x: string]: any; }) => {
                works.push(
                    {
                        id: element["Id"],
                        name: element["Name"],
                        score: element["Score"],
                        max: element["Max"] ?? 0
                    }
                );
            });
        });
}
</script>

<template>
    <main>
        <Header />
        <div class="main__content">
            <Content>
                <WorksList :items="works" />
                <div class="new_work_title" v-if="studentId <= 0">
                    <Typography tag="p" size="l">Новая работа:</Typography>
                    <WorkSlot :newWork="true" />
                </div>
            </Content>
        </div>
    </main>
</template>

<style scoped>
.new_work_title {
    color: var(--main-on-default);
    display: flex;
    align-items: center;
    flex-direction: column;
}
</style>