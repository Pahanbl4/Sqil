import { DateFormatPipe } from './../../shared/pipes/date-format.pipe';
import { ItemsService } from './../../shared/utils/items.service';
import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IUser, IUserDetails, IUserEdit } from './../../shared/interfaces';
import { Component, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'editUser-modal',
  template: `
  <div bsModal #editUserModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" *ngIf="selectedUserUpdateLoaded">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>EDIT User</h4>
          </div>
          <div class="modal-body">
                  <form #editUserForm="ngForm" novalidate>
                      <div class="form-group">
                          <div class="row">
                              <div class="col-md-12">
                                  <div class="col-md-6">
                                      <label class="control-label">First Name</label>
                                      <input type="text" 
                                             class="form-control" 
                                             id="firstMidName" 
                                             required 
                                             maxlength="50" 
                                             [(ngModel)]="userEdit.firstMidName" 
                                             name="firstMidName" 
                                             #firstMidName="ngModel">
                                      <div *ngIf="firstMidName.errors && (firstMidName.dirty || firstMidName.touched)" class="alert alert-danger">
                                          <div [hidden]="!firstMidName.errors.required">
                                              First Name is required!
                                          </div>                                  
                                          <div [hidden]="!firstMidName.errors.maxlength">
                                              First Name cannot be more than 50 characters long.
                                          </div>
                                      </div>
                                  </div>
                                  <div class="col-md-6">
                                      <label class="control-label">Last Name</label>
                                      <input type="text" 
                                             class="form-control" 
                                             id="lastName" 
                                             required 
                                             maxlength="50" 
                                             [(ngModel)]="userEdit.lastName" 
                                             name="lastName" 
                                             #lastName="ngModel">
                                      <div *ngIf="lastName.errors && (lastName.dirty || lastName.touched)" class="alert alert-danger">
                                          <div [hidden]="!lastName.errors.required">
                                              Last Name is required!
                                          </div>                                     
                                          <div [hidden]="!lastName.errors.maxlength">
                                              Last Name cannot be more than 50 characters long.
                                          </div>
                                      </div>
                                  </div>
                              </div>                   
                          </div>
                          <br />
                          <div class="row">
                              <div class="col-md-12">
                                  <div class="col-md-6">
                                      <label class="control-label">Hire Date</label>
                                      <datepicker [(ngModel)]="userEdit.hireDate" name="hireDate" [showWeeks]="false"></datepicker>
                                  </div>
                                  
                              </div>
                          </div>  
                          <div class="row">
                              <div class="col-md-12">  
                                  <label class="control-label">ROles</label>  
                                  <br />                                         
                                  <div *ngFor="let role of userEdit.assignedRoles" class="col-md-4">
                                      <input type="checkbox"
                                              [id]="role.roleID"
                                              [checked]="role.assigned"
                                              (change)="role.assigned = !role.assigned" />
                                      <label [for]="role.roleID">
                                          {{role.roleID}}: {{role.title}}   
                                      </label>   
                                  </div>  
                              </div>
                          </div>  
                      </div>                    
                      <button type="submit" 
                              class="btn btn-default" 
                              [disabled]="!editUserForm.form.valid"
                              (click)="updateUser(editUserForm)">Update</button>
              </form>  
          </div>
      </div>
  </div>
</div>
  `,
})

export class EditUserModalComponent {    
  @ViewChild('editUserModal') public editUserModal: ModalDirective;
  @Output() userUpdated = new EventEmitter();
  userEdit: IUserEdit;    
  user: IUser;
  selectedUserUpdateLoaded: boolean = false;
  selectedRoles: string[] = []; // Initialize

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private itemsService: ItemsService) { }  

    loadUserForUpdate(id: number) {
        this.dataService.getUser(id)
          .subscribe((user: IUserEdit) => {
              this.userEdit = this.itemsService.getSerialized<IUserEdit>(user);
              this.userEdit.hireDate = new Date(this.userEdit.hireDate.toString());
              this.selectedUserUpdateLoaded = true;
              this.editUserModal.show();
          },
          error => {
              this.notificationService.printErrorMessage('Failed to load user. ' + error);
          });
    }
  
    updateUser(editUserForm: NgForm) {
        // Find checkbox(es) that are checked and add to an array
       this.userEdit.assignedRoles.forEach(item => {
         if (item.assigned)
             this.selectedRoles.push(item.roleID.toString());
         });
       this.user = this.itemsService.getSerialized<IUser>(this.userEdit);
       this.user.selectedRoles = this.selectedRoles;
       
       this.dataService.updateUser(this.user)
           .subscribe(() => {
               this.notificationService.printSuccessMessage('user has been updated');
               this.closeModal();
               this.userUpdated.emit({});
           },
           error => {
               this.notificationService.printErrorMessage('Failed to update user. ' + error);
           });
   
          this.selectedRoles = []; // Reset, so it will not add up the previously selected courses
       }
   

  openModal(id: number){
    this.loadUserForUpdate(id);  
    this.editUserModal.show();
  }

  closeModal() {
    this.editUserModal.hide();
  }
}