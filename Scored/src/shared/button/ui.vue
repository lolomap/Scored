<script lang="ts" setup>
import { Typography } from '@/shared/typography/';

interface Props {
    color?: 'default' | 'alert' | 'normal' | 'good' | 'great';
    decoration?: 'default' | 'outline' | 'none';
    size?: 'L' | 'M' | 'S';
    disabled?: boolean;
}

const props = defineProps<Props>();
const {
    color = 'default', 
    decoration = 'default', 
    size = 'M', 
    disabled = false
} = props;

const classes = ['button', `size_${size}`, `decoration_${decoration}`, `color_${color}`];
</script>

<template>
    <button :class="classes" :disabled="disabled">
        <slot name="icon"></slot>
        <Typography class="button__text" tag="p" size="s"><slot></slot></Typography>
    </button>
</template>

<style scoped>
.button {
    display: flex;
    align-items: center;
    width: 100%;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    color: var(--main-on-default);
}

.button__text {
    width: 100%;
    text-align: center;
}

.button.size_M {
    grid-gap: 8px;
    padding: 8px;
}

.button.color_default {
    background-color: var(--main-default);
}

.button.color_alert {
    background-color: var(--main-alert);
}

.button.color_normal {
    background-color: var(--main-normal);
}

.button.color_good {
    background-color: var(--main-good);
}

.button.color_great {
    background-color: var(--main-great);
}

</style>