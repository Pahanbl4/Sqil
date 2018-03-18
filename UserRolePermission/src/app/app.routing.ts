
import { Routes, RouterModule } from '@angular/router';
 
import { HeaderComponent } from "./core/header/header.component";
import { HomeComponent } from './core/home/home.component';
import { PermissionListComponent } from './permissions/permission-list.component';
import { RoleListComponent } from './roles/role-list.component';
import { UserListComponent } from './users/user-list.component';

const appRoutes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },

    { path: 'permissions', component: PermissionListComponent},
    { path: 'roles', component: RoleListComponent },
    { path: 'users', component: UserListComponent },
   
];
 
export const routing = RouterModule.forRoot(appRoutes);
 
export const routedComponents = [
    HeaderComponent,
    HomeComponent
];
//export const routedComponents = [AboutComponent, IndexComponent, ContactComponent, LoginComponent, RegisterComponent];