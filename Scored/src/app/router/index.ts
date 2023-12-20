import { createRouter, createWebHistory } from 'vue-router'
import { Home } from '@/pages/home_page'
import { Grades } from '@/pages/grades_page/'
import { Works } from '@/pages/works_page'
import { Auth } from '@/pages/auth_page'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
        path: '/',
        name: 'home',
        component: Home
    },
    {
        path: '/auth',
        name: "auth",
        component: Auth
    },
    {
      path: '/grades',
      name: 'grades',
      component: Grades
    },
    {
        path: '/works',
        name: 'works',
        component: Works
    }
  ]
})

export default router
