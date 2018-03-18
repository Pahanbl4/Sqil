import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IPermission } from './../../shared/interfaces';
import { Component, OnInit, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'createPermission-modal',
  template: `
  <div bsModal #createPermissionModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>NEW Permission</h4>
          </div>
          <div class="modal-body">
              <form #createPermissionForm="ngForm" novalidate>
                  <div class="form-group">
                      <div class="row">
                          <div class="col-md-12">
                              <div class="col-md-6">
                                  <label class="control-label">permission Name</label>
                                  <input type="text" 
                                         class="form-control" 
                                         id="permissionName" 
                                         required 
                                         maxlength="50" 
                                         [(ngModel)]="permission.permissionName" 
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
                          [disabled]="!createPermissionForm.form.valid"
                          (click)="addPermission(createPermissionForm)">Submit</button>
              </form>  
          </div>
      </div>
  </div>
</div>
  `,
})

export class CreatePermissionModalComponent {
  @ViewChild('createPermissionModal') public createPermissionModal: ModalDirective;
  @Input() permission: IPermission;
  @Output() permissionCreated = new EventEmitter();

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService) { }  
      
  addPermission(form: NgForm) {   
    this.dataService.createPermission(this.permission)
      .subscribe((permissionCreated) => {
        this.notificationService.printSuccessMessage(permissionCreated.permissionName  + ' has been created');
        this.permissionCreated.emit({});
        this.closeModal();
    },
    error => {
      this.notificationService.printErrorMessage('Failed to create a new Permission. ' + error);
    })

    form.reset();
  }
  
  openModal(){
    this.createPermissionModal.show();
  }

  closeModal() {
    this.createPermissionModal.hide();
  }
}