<script setup lang="ts">
import { Header } from '@/widgets/header';
import { DisciplineList } from '@/features/discipline_list';
import { computed, reactive } from 'vue';
import { Content } from '@/widgets/content';
import { type DisciplineModel } from '@/entities/discipline';
import { useEntityStore } from '@/entities/store';

const entityStore = useEntityStore();
const studentId = computed(() => entityStore.studentId);

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify(
        {
            "studentId": studentId.value,
            "teacherId": entityStore.user.type == 'teacher' ? entityStore.user.id : -1
        }
    )
};

let disciplines = reactive(Array<DisciplineModel>());
disciplines.pop();

fetch('https://localhost:7083/grades', requestOptions)
    .then(response => response.json())
    .then(data => {
        data.forEach((element: { [x: string]: { [x: string]: any; }[]; }) => {
            disciplines.push(
                {
                    id: element["rows"][0]["Id"],
                    name: element["rows"][0]["Discipline"],
                    score: element["rows"][0]["Scores"] ?? 0,
                    mark: element["rows"][0]["Mark"] ?? 2
                }
            );
        });
    });

</script>

<template>
    <main>
        <Header />
        <div class="main__content">
            <Content>
                <DisciplineList :items="disciplines" />
            </Content>
        </div>
    </main>
</template>

<style scoped>

</style>