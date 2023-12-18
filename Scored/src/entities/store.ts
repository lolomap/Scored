import { ref, reactive } from 'vue';
import { defineStore } from 'pinia';
import { type UserModel } from '@/entities/user';

export const useEntityStore = defineStore('entity', () => {
    const studentId = ref(1);
    const disciplineId = ref(0);

    let __user__: UserModel = {
        id: 0,
        type: 'student',
        name: 'Blank'
    };
    let user = reactive(__user__);

    const setStudentId = (value: number) => studentId.value = value;
    const setDisciplineId = (value: number) => disciplineId.value = value;

    return {studentId, disciplineId, user, setStudentId, setDisciplineId};
});