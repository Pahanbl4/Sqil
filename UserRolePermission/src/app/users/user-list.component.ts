import { Component, OnInit, ViewChild, ViewContainerRef, trigger, state, style, transition, animate } from '@angular/core';
import { Pagination, PaginatedResult, IUser, IRole, IUserDetails, IUserEdit } from './../shared/interfaces';
import { ConfigService } from './../shared/utils/config.service';
import { NotificationService } from './../shared/utils/notification.service';
import { ItemsService } from './../shared/utils/items.service';
import { DataService } from './../shared/services/data.service';
import { DateFormatPipe } from '../shared/pipes/date-format.pipe';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-permissions',
  templateUrl: './user-list.component.html',
  animations: [    
    trigger('flyInOut', [
      transition(':enter', [
        style({transform: 'translateX(-100%)'}),
        animate(500, style({transform: 'translateX(0)'}))
      ]),
      transition(':leave', [
        style({transform: 'translateX(0)'}),
        animate(500, style({transform: 'translateX(100%)'}))
      ])
    ])
  ]
})

export class UserListComponent implements OnInit {
  public itemsPerPage: number = 8;
  public totalItems: number = 0;
  public currentPage: number = 1;
  users: IUser[];
  apiHost: string; 
  user: IUser;

  constructor(
    private dataService: DataService,
    private itemsService: ItemsService,
    private notificationService: NotificationService,
    private configService: ConfigService) { }

  ngOnInit() {
    this.apiHost = this.configService.getApiHost();
    this.loadUsers();
    this.initializeCreateUser();
  }

  loadUsers() {
    this.dataService.getUsers(this.currentPage, this.itemsPerPage)
        .subscribe((res: PaginatedResult<IUser[]>) => {
            this.users = res.result;
            this.totalItems = res.pagination.TotalItems;         
        },
        error => {           
            this.notificationService.printErrorMessage('Failed to load users. ' + error);
        });
  }

  initializeCreateUser() {    
    const obj = {}; // Initialization
    this.user = this.itemsService.getSerialized<IUser>(obj);
    this.loadAllRoles(); // Load all courses and add it to this.instructor.courses
  }

  loadAllRoles() {
    this.dataService.getAllRoles()
        .subscribe((roles: IRole[]) => {
            this.user.roles = roles;       
        },
        error => {           
            this.notificationService.printErrorMessage('Failed to load all roles. ' + error);
        });
  }
  
  removeUser(user: IUser) {
    this.notificationService.openConfirmationDialog('Are you sure you want to delete this user?',
        () => {           
            this.dataService.deleteUser(user.id)
                .subscribe(() => {
                    this.itemsService.removeItemFromArray<IUser>(this.users, user);
                    this.notificationService.printSuccessMessage(user.firstMidName + ' ' + user.lastName + ' has been deleted.');                  
                },
                error => {                  
                    this.notificationService.printErrorMessage('Failed to delete ' + user.firstMidName + user.lastName + ' '  + error);
                });
        });
  }

  pageChanged(event: any): void {
    this.currentPage = event.page;
    this.loadUsers();
  };
}