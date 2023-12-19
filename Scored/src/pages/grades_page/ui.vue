<script setup lang="ts">
import { Header } from '@/widgets/header';
import { DisciplineList } from '@/features/discipline_list';
import { computed, reactive, ref } from 'vue';
import { Content } from '@/widgets/content';
import { type DisciplineModel } from '@/entities/discipline';
import { useEntityStore } from '@/entities/store';

const entityStore = useEntityStore();
const user = computed(() => entityStore.user);
const studentId = computed(() => entityStore.studentId);

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: ''
};

let disciplines = reactive(Array<DisciplineModel>());
disciplines.pop();

let teacherDisciplines = ref(false);
if (studentId.value > 0)
{
    requestOptions.body = JSON.stringify(
        {
            "studentId": studentId.value,
            "teacherId": entityStore.user.type == 'teacher' ? entityStore.user.id : -1
        }
    );

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
}
else if (user.value.type == 'teacher')
{
    teacherDisciplines.value = true;
    requestOptions.body = JSON.stringify(
        {
            "teacherId": entityStore.user.id
        }
    )

    fetch('https://localhost:7083/disciplines', requestOptions)
        .then(response => response.json())
        .then(data => {
            data["rows"].forEach((element: { [x: string]: any; }) => {
                disciplines.push(
                    {
                        id: element["Id"],
                        name: element["Name"],
                        score: 0,
                        mark: 0
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
                <DisciplineList :items="disciplines" :noStudent="teacherDisciplines" />
            </Content>
        </div>
    </main>
</template>

<style scoped>

</style>