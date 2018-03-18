import { DateFormatPipe } from './../../shared/pipes/date-format.pipe';
import { ItemsService } from './../../shared/utils/items.service';
import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IPermission, IPermissionDetails } from './../../shared/interfaces';
import { Component, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'editPermission-modal',
  template: `
  <div bsModal #editPermissionModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" *ngIf="selectedPermissionLoaded">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>EDIT Permission</h4>
          </div>
          <div class="modal-body">
                  <form #editPermissionForm="ngForm" novalidate>
                      <div class="form-group">
                          <div class="row">
                              <div class="col-md-12">
                                  <div class="col-md-6">
                                      <label class="control-label">Permission Name</label>
                                      <input type="text" 
                                             class="form-control" 
                                             id="permissionName" 
                                             required 
                                             maxlength="50" 
                                             [(ngModel)]="permissionDetails.permissionName" 
                                             name="permissionName" 
                                             #permissionName="ngModel">
                                      <div *ngIf="permissionName.errors && (permissionName.dirty || permissionName.touched)" class="alert alert-danger">
                                          <div [hidden]="!permissionName.errors.required">
                                              First Name is required!
                                          </div>                                  
                                          <div [hidden]="!permissionName.errors.maxlength">
                                              First Name cannot be more than 50 characters long.
                                          </div>
                                      </div>
                                  </div>
                                  
                              </div>                   
                          </div>
                          <br />
                         
                      </div>                    
                      <button type="submit" 
                              class="btn btn-default" 
                              [disabled]="!editPermissionForm.form.valid"
                              (click)="updatePermission(editPermissionForm)">Update</button>
                  </form>  
              </div>
      </div>
  </div>
</div>
  `,
})

export class EditPermissionModalComponent {    
  @ViewChild('editPermissionModal') public editPermissionModal: ModalDirective;
  @Output() permissionUpdated = new EventEmitter();
  permissionDetails: IPermissionDetails;    
  selectedPermissionLoaded: boolean = false;
  permission: IPermission;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private itemsService: ItemsService) { }  

    loadPermissionForUpdate(id: number) {
        this.dataService.getPermissionDetails(id)
          .subscribe((permission: IPermissionDetails) => {
              this.permissionDetails = this.itemsService.getSerialized<IPermissionDetails>(permission);
        
              this.selectedPermissionLoaded = true;
          },
          error => {
              this.notificationService.printErrorMessage('Failed to load permission. ' + error);
          });
    }
  
    updatePermission(editScheduleForm: NgForm) {
        this.permission = this.itemsService.getSerialized<IPermission>(this.permissionDetails);
        this.dataService.updatePermission(this.permission)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('Permission has been updated');
                this.closeModal();
                this.permissionUpdated.emit({});
            },
            error => {
                this.notificationService.printErrorMessage('Failed to update Permission. ' + error);
            });
        }

  openModal(id: number){
    this.loadPermissionForUpdate(id);  
    this.editPermissionModal.show();
  }

  closeModal() {
    this.editPermissionModal.hide();
  }
}