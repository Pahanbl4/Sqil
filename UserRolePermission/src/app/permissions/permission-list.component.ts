import { Component, OnInit, ViewChild, ViewContainerRef, trigger, state, style, transition, animate } from '@angular/core';
import { IPermission, IPermissionDetails, Pagination, PaginatedResult } from './../shared/interfaces';
import { ConfigService } from './../shared/utils/config.service';
import { NotificationService } from './../shared/utils/notification.service';
import { ItemsService } from './../shared/utils/items.service';
import { DataService } from './../shared/services/data.service';
import { DateFormatPipe } from '../shared/pipes/date-format.pipe';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-permissions',
  templateUrl: './permission-list.component.html',
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

export class PermissionListComponent implements OnInit {
  public itemsPerPage: number = 8;
  public totalItems: number = 0;
  public currentPage: number = 1;
  permissions: IPermission[];
  apiHost: string; 
  permission: IPermission;

  constructor(
    private dataService: DataService,
    private itemsService: ItemsService,
    private notificationService: NotificationService,
    private configService: ConfigService) { }

  ngOnInit() {
    this.apiHost = this.configService.getApiHost();
    this.loadPermissions();
    this.initializeCreatePermission();
  }

  loadPermissions() {
    this.dataService.getPermissions(this.currentPage, this.itemsPerPage)
        .subscribe((res: PaginatedResult<IPermission[]>) => {
            this.permissions = res.result;
            this.totalItems = res.pagination.TotalItems;         
        },
        error => {           
            this.notificationService.printErrorMessage('Failed to load schedules. ' + error);
        });
  }

  initializeCreatePermission() {
    const obj = { permissionName: '' };
    this.permission = this.itemsService.getSerialized<IPermission>(obj);
  }

  removePermission(permission: IPermission) {
    this.notificationService.openConfirmationDialog('Are you sure you want to delete this Permission?',
        () => {           
            this.dataService.deletePermission(permission.id)
                .subscribe(() => {
                    this.itemsService.removeItemFromArray<IPermission>(this.permissions, permission);
                    this.notificationService.printSuccessMessage(permission.permissionName +  ' has been deleted.');                  
                },
                error => {                  
                    this.notificationService.printErrorMessage('Failed to delete ' + permission.permissionName  + ' '  + error);
                });
        });
  }

  pageChanged(event: any): void {
    this.currentPage = event.page;
    this.loadPermissions();
  };
}