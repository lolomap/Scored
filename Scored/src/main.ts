import '@/app/main.css';

import { createApp, watch } from 'vue';
import { createPinia } from 'pinia';
import { aadClientId } from './secrets';


import App from '@/app/App.vue';
import router from '@/app/router';

const app = createApp(App);

const pinia = createPinia();

app.use(pinia);
app.use(router);

app.mount('#app');
