import { Component, OnInit, ViewChild, ViewContainerRef, trigger, state, style, transition, animate } from '@angular/core';
import { IPermission, IPermissionDetails, Pagination, PaginatedResult, IRole } from './../shared/interfaces';
import { ConfigService } from './../shared/utils/config.service';
import { NotificationService } from './../shared/utils/notification.service';
import { ItemsService } from './../shared/utils/items.service';
import { DataService } from './../shared/services/data.service';
import { DateFormatPipe } from '../shared/pipes/date-format.pipe';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
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

export class RoleListComponent implements OnInit {
  public itemsPerPage: number = 8;
  public totalItems: number = 0;
  public currentPage: number = 1;
  roles: IRole[];
  apiHost: string; 
  role: IRole;

  constructor(
    private dataService: DataService,
    private itemsService: ItemsService,
    private notificationService: NotificationService,
    private configService: ConfigService) { }

  ngOnInit() {
    this.apiHost = this.configService.getApiHost();
    this.loadRoles();
    this.initializeCreateRole();
  }

  loadRoles() {
    this.dataService.getRoles(this.currentPage, this.itemsPerPage)
        .subscribe((res: PaginatedResult<IRole[]>) => {
            this.roles = res.result;
            this.totalItems = res.pagination.TotalItems;         
        },
        error => {           
            this.notificationService.printErrorMessage('Failed to load roles. ' + error);
        });
  }

  initializeCreateRole() {    
    const obj = { roleID: '', title: '' };
    this.role = this.itemsService.getSerialized<IRole>(obj); 
  }  

  removeRole(role: IRole) {
    this.notificationService.openConfirmationDialog('Are you sure you want to delete this Role?',
        () => {           
            this.dataService.deleteRole(role.roleID)
                .subscribe(() => {
                    this.itemsService.removeItemFromArray<IRole>(this.roles, role);
                    this.notificationService.printSuccessMessage(role.title + ' has been deleted.');                  
                },
                error => {                  
                    this.notificationService.printErrorMessage('Failed to delete ' + role.title + ' '  + error);
                });
        });
  }

 

  pageChanged(event: any): void {
    this.currentPage = event.page;
    this.loadRoles();
  };
}