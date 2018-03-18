import { DateFormatPipe } from './../../shared/pipes/date-format.pipe';
import { ItemsService } from './../../shared/utils/items.service';
import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IUserDetails } from './../../shared/interfaces';
import { Component, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'detailUser-modal',
  template: `
  <div bsModal #detailUserModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" *ngIf="selectedUserLoaded">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                   <span aria-hidden="true">&times;</span>
              </button>
              <h4>COURSE(S) TAUGHT BY SELECTED User</h4>
          </div>
          <div class="modal-body">
               <div class="form-group">
                   <div class="row">
                       <table class="table table-hover">
                           <thead>
                               <tr>
                                   <th>Number</th>
                                   <th>Title</th>
                         
                                   <th></th>
                               </tr>
                           </thead>
                           <tbody>
                               <tr *ngFor="let role of userRoleDetails.roles">
                                   <td>{{role.roleID}}</td>
                                   <td>{{role.title}}</td>
                                                                
                                   <td>
                                       <button class="btn btn-primary" (click)="viewUserRolePermissionDetails(role.roleID)"> 
                                       <i class="fa fa-info-circle" aria-hidden="true"></i> Select</button>
                                   </td> 
                               </tr>                                  
                           </tbody>
                       </table>      
                   </div>                
               </div> 
               <div *ngIf="selectedPermissionDetailsLoaded">
                   <h4>Permission(S) ENROLLED IN SELECTED role</h4>
                   <table class="table table-hover">                    
                       <thead>
                           <tr>
                               <th>Name</th>
                          
                           </tr>
                       </thead>
                       <tbody>
                           <tr *ngFor="let user of userRolePermissionDetails.enrollments">
                               <td>{{user.fullName}}</td>
                           
                           </tr>                                                    
                       </tbody>
                   </table>  
               </div>                          
          </div>
      </div>
  </div>
</div>
  `,
})

export class DetailUserModalComponent {    
  @ViewChild('detailUserModal') public detailUserModal: ModalDirective;
 userRoleDetails: IUserDetails;   
  userRolePermissionDetails: IUserDetails;   
  selectedUserLoaded: boolean = false;  
  selectedPermissionDetailsLoaded: boolean = false;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private itemsService: ItemsService) { }  

    viewUserRoleDetails(id: number) {
        this.selectedPermissionDetailsLoaded = false;
    
        this.dataService.getUserRoleDetails(id)
            .subscribe((user: IUserDetails) => {          
                this.userRoleDetails = this.itemsService.getSerialized<IUserDetails>(user);
                this.selectedUserLoaded = true;
                this.detailUserModal.show();
            },
            error => {
                this.notificationService.printErrorMessage('Failed to load role details. ' + error);
            });
      }

      viewUserRolePermissionDetails(id: number) {    
        this.dataService.getUserRolePermissionDetails(id)
            .subscribe((user: IUserDetails) => {          
                this.userRolePermissionDetails = this.itemsService.getSerialized<IUserDetails>(user);
                console.log(this.userRolePermissionDetails)
                this.selectedPermissionDetailsLoaded = true;
            },
            error => {
                this.notificationService.printErrorMessage('Failed to load permission details. ' + error);
            });
      }
  
  openModal(id: number){
    this.viewUserRoleDetails(id);  
    this.detailUserModal.show();
  }

  closeModal() {
    this.detailUserModal.hide();
  }
}