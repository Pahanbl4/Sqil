import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IRole} from './../../shared/interfaces';
import { Component, OnInit, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'createRole-modal',
  template: `
  <div bsModal #createRoleModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>NEW Role</h4>
          </div>
          <div class="modal-body">
              <form #createRoleForm="ngForm" novalidate>
                  <div class="form-group">
                      <div class="row">
                          <div class="col-md-12">
                              <div class="col-md-6">
                                  <label class="control-label">Role ID</label>
                                  <input type="text" 
                                         class="form-control" 
                                         id="roleID" 
                                         required 
                                         maxlength="50" 
                                         [(ngModel)]="role.roleID" 
                                         name="roleID" 
                                         #roleID="ngModel">
                                  <div *ngIf="roleID.errors && (roleID.dirty || roleID.touched)" class="alert alert-danger">
                                      <div [hidden]="!roleID.errors.required">
                                      Role ID is required!
                                      </div>  
                                  </div>
                              </div>
                              <div class="col-md-6">
                                  <label class="control-label">Title</label>
                                  <input type="text" 
                                         class="form-control" 
                                         id="title" 
                                         required 
                                         maxlength="20" 
                                         [(ngModel)]="role.title" 
                                         name="title" 
                                         #title="ngModel">
                                  <div *ngIf="title.errors && (title.dirty || title.touched)" class="alert alert-danger">
                                      <div [hidden]="!title.errors.required">
                                          Title is required!
                                      </div>                                     
                                      <div [hidden]="!title.errors.maxlength">
                                          Title cannot be more than 20 characters long.
                                      </div>
                                  </div>
                              </div>
                          </div>                   
                      </div>
                      <br />
                       
                  </div>                    
                  <button type="submit" 
                          class="btn btn-default" 
                          [disabled]="!createRoleForm.form.valid"
                          (click)="addRole(createRoleForm)">Submit</button>
              </form>  
          </div>
      </div>
  </div>
</div>
  `,
})

export class CreateRoleModalComponent {
  @ViewChild('createRoleModal') public createRoleModal: ModalDirective;
  @Input() role: IRole;
  @Output() roleCreated = new EventEmitter();
  
  constructor(
    private dataService: DataService,
    private notificationService: NotificationService) { }        

    addRole(form: NgForm) {   
        this.dataService.createRole(this.role)
            .subscribe((roleCreated) => {
                this.notificationService.printSuccessMessage(roleCreated.title + ' has been created');
                this.roleCreated.emit({});
                this.closeModal();
            },
            error => {
                this.notificationService.printErrorMessage('Failed to create a new role. ' + error);
            })

        form.reset();
        }

  openModal(){
    this.createRoleModal.show();
  }

  closeModal() {
    this.createRoleModal.hide();
  }
}