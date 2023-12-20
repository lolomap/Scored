<script setup lang="ts">
import { Field } from '@/shared/field';
import { Typography } from '@/shared/typography';
import { Header } from '@/widgets/header';
import { Content } from '@/widgets/content';
import { LeftBar } from '@/widgets/left_bar';
import { DisciplineList } from '@/features/discipline_list';
import { UserList } from '@/features/user_list';

import { useEntityStore } from '@/entities/store';
import { type DisciplineModel } from '@/entities/discipline';
import { type UserModel } from '@/entities/user';

import { computed, reactive, ref } from 'vue';
import router from '@/app/router';

const entityStore = useEntityStore();
const studentId = computed(() => entityStore.studentId);
const studentName = computed(() => entityStore.studentName);

const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: ''
};

const disciplines = reactive(Array<DisciplineModel>());
disciplines.pop();

const teacherDisciplines = ref(false);
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
else if (entityStore.user.type == 'teacher')
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


const students = reactive(Array<UserModel>());
students.pop();

const onSearchStudents = (value: string) => {
    students.length = 0;

    requestOptions.body = JSON.stringify(
        {
            "searchQuery": value
        }
    );

    fetch('https://localhost:7083/search_students', requestOptions)
        .then(response => response.json())
        .then(data => {
            data["rows"].forEach((element: { [x: string]: any; }) => {
                students.push(
                    {
                        id: element["Id"],
                        type: 'student',
                        name: element["Name"]
                    }
                );
            });
        });
};

const onStudentClick = (student: UserModel) => {
    entityStore.setStudent(student.id, student.name)
    router.go(0);
};

</script>

<template>
    <main>
        <Header />
        <div class="main__content">
            <Content>
                <template #left-bar>
                    <LeftBar v-if="entityStore.user.type == 'teacher'">
                        <Field :onEnter="onSearchStudents" />
                        <div class="search_results">
                            <UserList :items="students" :item_action="onStudentClick" />
                        </div>
                    </LeftBar>
                </template>
                <Typography tag="h2" style="color: var(--main-on-default);">{{ studentName }}</Typography>
                <DisciplineList :items="disciplines" :noStudent="teacherDisciplines" />
            </Content>
        </div>
    </main>
</template>

<style scoped>

.search_results {
    padding: 16px 0px;
    color: var(--main-on-default);
}

</style>