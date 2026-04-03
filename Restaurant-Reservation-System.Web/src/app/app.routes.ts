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
            { path: 'users', component: AdminPanelUsers }
        ]
    },
    {
        path: 'about',
        component: About
    }
];
