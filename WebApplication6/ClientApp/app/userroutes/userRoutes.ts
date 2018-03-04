import { Routes } from '@angular/router';


import { AppComponent } from '../components/app/app.component';
import { NavMenuComponent } from '../components/navmenu/navmenu.component';
import { HomeComponent } from '../components/home/home.component';
import { FetchDataComponent } from '../components/fetchdata/fetchdata.component';
import { CounterComponent } from '../components/counter/counter.component';
import { AddUserComponent } from '../components/adduser/adduser.component';
import { ViewUserComponent } from '../components/viewuser/viewuser.component';
import { EditUserComponent } from '../components/edituser/edituser.component';


export const USERS_ROUTES: Routes =
	[
		{ path: '', redirectTo: 'home', pathMatch: 'full' },
		{ path: 'home', component: HomeComponent },
		{ path: 'counter', component: CounterComponent },
		{ path: 'fetch-data', component: FetchDataComponent },
		{ path: 'add-user', component: AddUserComponent },
		{ path: 'view-user', component: ViewUserComponent },
		{ path: 'edit-user/:userId', component: EditUserComponent },
		{ path: '**', redirectTo: 'home' }
	];