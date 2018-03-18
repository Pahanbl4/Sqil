import { CreateRoleModalComponent } from './roles/modals/createRole-modal';
import { DetailPermissionModalComponent } from './permissions/modals/detailPermission-modal';
import { ItemsService } from './shared/utils/items.service';
import { DataService } from './shared/services/data.service';
import { ConfigService } from './shared/utils/config.service';
import { NotificationService } from './shared/utils/notification.service';

import { DateFormatPipe } from './shared/pipes/date-format.pipe';
import { routing, routedComponents } from './app.routing';
import { AppErrorHandler } from './app.error-handler';
import { ErrorHandler } from '@angular/core';

import { BrowserModule, Title } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule  } from '@angular/http';
import { ToastyModule } from 'ng2-toasty';
import { PaginationModule, ModalModule, DatepickerModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { HeaderComponent } from './core/header/header.component';
import { HomeComponent } from './core/home/home.component';
import { PermissionListComponent } from './permissions/permission-list.component';
import { RoleListComponent } from './roles/role-list.component';
import { UserListComponent } from './users/user-list.component';
import { CreatePermissionModalComponent } from './permissions/modals/createPermission-modal';
import { EditPermissionModalComponent } from './permissions/modals/editPermission-modal';
import { EditRoleModalComponent } from './roles/modals/editRole-modal';
import { CreateUserModalComponent } from './users/modals/createUser-modal';
import { DetailUserModalComponent } from './users/modals/detailUser-modal';
import { EditUserModalComponent } from './users/modals/editUser-modal';


@NgModule({
  declarations: [
    AppComponent, 
    HomeComponent,    
    DateFormatPipe,
    HeaderComponent, 
    routedComponents,
    PermissionListComponent,
    RoleListComponent,
    UserListComponent,
    CreatePermissionModalComponent,
    DetailPermissionModalComponent,
    EditPermissionModalComponent,
    CreateRoleModalComponent,
    EditRoleModalComponent,
    CreateUserModalComponent,
    DetailUserModalComponent,
    EditUserModalComponent
  ],
  imports: [
    routing,
    HttpModule, 
    FormsModule,     
    BrowserModule,
    BrowserAnimationsModule,   
    ModalModule.forRoot(), 
    ToastyModule.forRoot(),
    PaginationModule.forRoot(),
    DatepickerModule.forRoot()
  ],
  providers: [
    Title,
    DataService,
    ItemsService,
    ConfigService,
    NotificationService,  
    { provide: ErrorHandler, useClass: AppErrorHandler },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
