import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { AddUserComponent } from './components/adduser/adduser.component';
import { ViewUserComponent } from './components/viewuser/viewuser.component';
import { EditUserComponent } from './components/edituser/edituser.component';

import { USERS_ROUTES } from './userroutes/userRoutes';
import { UsersTypeDataService } from './services/userstype.dataservice';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
		HomeComponent,
		AddUserComponent,
		EditUserComponent,
		ViewUserComponent
	],

	providers: [UsersTypeDataService],

	exports: [
		FormsModule,
		ReactiveFormsModule

	],

    imports: [
        CommonModule,
        HttpModule,
		FormsModule,
		ReactiveFormsModule,
		RouterModule.forRoot(USERS_ROUTES)
    ]
})
export class AppModuleShared {
}
