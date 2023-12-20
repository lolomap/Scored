<script setup lang="ts">
import { Content } from '@/widgets/content';
import { Header } from '@/widgets/header';
import { Typography } from '@/shared/typography';
import { Field } from '@/shared/field';
import { Button } from '@/shared/button';
import { aadClientId } from '@/secrets';
import { useEntityStore } from '@/entities';
import router from '@/app/router';
import type { UserModel } from '@/entities/user';

const entityStore = useEntityStore();
let field_input = '';



const onChangeAuth = (value: string) => field_input = value;
const onSubmit = () => {
    const requestOptions = {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "login": field_input
            }
        )
    };

    let user: UserModel = {
        id: 0,
        type: 'student',
        name: ''
    };
    fetch('https://localhost:7083/auth', requestOptions)
        .then(response => response.json())
        .then(data => {
            user.id = data['id'];
            user.type = data['type'] == 'Student' ? 'student' : 'teacher';
            user.name = data['surname'] + ' ' + data['name'][0] + '.' + data['patronymic'][0] + '.';

            entityStore.user = user;
            if (user.type == 'student') entityStore.setStudentId(user.id);
            else entityStore.setStudentId(0);
            router.push('/grades');
        });
};

</script>

<template>
    <Header />
    <div class="main__content">
        <Content>
            <div class="main__content_box">
                <Typography tag="h4">E-mail SFEDU.RU:</Typography>
                <Field 
                    class="auth_field" 
                    placeholder="example@sfedu.ru" 
                    :onChange="onChangeAuth"
                    :onEnter="onSubmit"
                />
                <Button 
                    class="auth_button" 
                    color="alert"
                    @click="onSubmit"
                >Авторизоваться</Button>
            </div>
        </Content>
    </div>
</template>

<style scoped>

.main__content_box {
    height: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    gap: 25px;
    color: var(--main-on-default)
}

.auth_field {
    width: 60%;
}

.auth_button {
    width: 250px;
}

</style>