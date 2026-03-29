import { Routes } from '@angular/router';
import { Home } from "./components/home/home";
import { Login } from './components/login/login';
import { Register } from './components/register/register';
import { AdminPanel } from './components/admin-panel/admin-panel';


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
        path: 'login',
        component: Login
    },
    {
        path: 'register',
        component: Register
    },
    {
        path: 'admin-panel',
        component: AdminPanel
    }
];
