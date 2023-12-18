<!-- 
<script lang="ts" setup>
import { Container } from '@/shared/container/';
import { Button } from '@/shared/button/';
import { Icon } from '@/shared/icon/'
import { Field } from '@/shared/field';
import { Typography } from '@/shared/typography';

const onChangeSearch = (value: string) => console.log(value);
const onSearch = (value: string) => console.log(value);
</script>

<template>
    <header class="header">
        <Container class="header_container">
            <div>
                <Button>
                    <template #icon>
                        <Icon type="menu" />
                    </template>
                    Кнопка
                </Button>
            </div>
        </Container>
    </header>

    <div>
        <Field
            placeholder="Найти студента"
            :onChange="onChangeSearch"
            :onEnter="onSearch"
        >
            <template #label>
                <Typography tag="span" size="s">Label</Typography>
            </template>
        </Field>
    </div>
</template>

<style scoped>
.header_container {
    display: flex;
    align-items: center;
    height: 72px;
}

.header {
    position: sticky;
    top: 0;
    left: 0;
    background: var(--header-surface);
    box-shadow: var(--shadow-default-s);
}
</style> -->