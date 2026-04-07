import { Routes } from '@angular/router';
import { Home } from "./components/home/home";
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { AdminPanel } from './components/admin-panel/admin-panel';
import { Profile } from './components/profile/profile';
import { Menus } from './components/menus/menus';
import { Reservations } from './components/admin-panel/reservations/reservations';
import { AdminPanelRestaurants } from './components/admin-panel/admin-panel-restaurants/admin-panel-restaurants';
import { About } from './components/about/about';
import { AdminPanelMenus } from './components/admin-panel/admin-panel-menus/admin-panel-menus';
import { AdminPanelUsers } from './components/admin-panel/admin-panel-users/admin-panel-users';
import { Contact } from './components/contact/contact';
import { WorkerPanel } from './components/worker-panel/worker-panel';
import { WorkerPanelUsers } from './components/worker-panel/worker-panel-users/worker-panel-users';
import { AdminPanelSchedule } from './components/admin-panel/admin-panel-schedule/admin-panel-schedule';
import { WorkerPanelReservations } from './components/worker-panel/worker-panel-reservations/worker-panel-reservations';
import { AdminPanelEmailjs } from './components/admin-panel/admin-panel-emailjs/admin-panel-emailjs';


export const routes: Routes = [
    {
        path: '',
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path: 'home',
        component: Home
    },
    {
        path: "menu",
        component: Menus
    },
    {
        path: 'login',
        component: Login
    },
    {
        path: 'register',
        component: Register
    },
    {
        path: 'profile',
        component: Profile
    },
    // {
    //     path: 'admin-panel',
    //     component: AdminPanel
    // }
    {
        path: 'admin-panel',
        component: AdminPanel,
        children: [
            { path: 'reservations', component: Reservations },
            { path: 'restaurants', component: AdminPanelRestaurants },
            { path: 'menus', component: AdminPanelMenus },
            { path: 'users', component: AdminPanelUsers },
            { path: 'schedules', component: AdminPanelSchedule },
            { path: 'email-js', component: AdminPanelEmailjs }
        ]
    },
    {
        path: 'worker-panel',
        component: WorkerPanel,
        children: [
            { path: 'reservations', component: WorkerPanelReservations },
            { path: 'users', component: WorkerPanelUsers }
        ]
    },
    {
        path: 'about',
        component: About
    },
    {
        path: 'contact',
        component: Contact 
    }
];
