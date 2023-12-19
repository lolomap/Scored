import { ref, reactive, watch } from 'vue';
import { defineStore } from 'pinia';
import { type UserModel } from '@/entities/user';

export const useEntityStore = defineStore('entity', {
    state: () => {
        const studentId = ref(0);
        const disciplineId = ref(0);
        const setStudentId = (value: number) => studentId.value = value;
        const setDisciplineId = (value: number) => disciplineId.value = value;

        const __user__: UserModel = {
            id: 0,
            type: 'student',
            name: 'Blank'
        };
        const user = reactive(__user__);

        return {studentId, disciplineId, user, setStudentId, setDisciplineId};
    },
    persist: true
});