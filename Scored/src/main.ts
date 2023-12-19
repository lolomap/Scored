import '@/app/main.css';

import { createApp, watch } from 'vue';
import { createPinia } from 'pinia';
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate';
import { aadClientId } from './secrets';


import App from '@/app/App.vue';
import router from '@/app/router';

const app = createApp(App);

const pinia = createPinia();
pinia.use(piniaPluginPersistedstate)

app.use(pinia);
app.use(router);

app.mount('#app');
