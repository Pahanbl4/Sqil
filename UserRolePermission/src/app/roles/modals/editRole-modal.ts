import { DateFormatPipe } from './../../shared/pipes/date-format.pipe';
import { ItemsService } from './../../shared/utils/items.service';
import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IRole } from './../../shared/interfaces';
import { Component, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'editRole-modal',
  template: `
  <div bsModal #editRoleModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" *ngIf="selectedRoleLoaded">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>EDIT Role</h4>
          </div>
          <div class="modal-body">
                  <form #editRoleForm="ngForm" novalidate>
                      <div class="form-group">
                          <div class="row">
                              <div class="col-md-12">
                                  <div class="col-md-6">
                                      <label class="control-label">Title</label>
                                      <input type="text" 
                                             class="form-control" 
                                             id="title" 
                                             required 
                                             maxlength="50" 
                                             [(ngModel)]="role.title" 
                                             name="title" 
                                             #title="ngModel">
                                      <div *ngIf="title.errors && (title.dirty || title.touched)" class="alert alert-danger">
                                          <div [hidden]="!title.errors.required">
                                              Title is required!
                                          </div>                                  
                                          <div [hidden]="!title.errors.maxlength">
                                              Title cannot be more than 50 characters long.
                                          </div>
                                      </div>
                                  </div>
                                  
                                  </div>
                              </div>                   
                          </div>
                          <br />
                           
                                      
                      <button type="submit" 
                              class="btn btn-default" 
                              [disabled]="!editRoleForm.form.valid"
                              (click)="updateRole(editRoleForm)">Update</button>
                  </form>  
              </div>
      </div>
  </div>
</div>
  `,
})

export class EditRoleModalComponent {    
  @ViewChild('editRoleModal') public editRoleModal: ModalDirective;
  @Output() roleUpdated = new EventEmitter();
  @Output() loadRole = new EventEmitter();
  selectedRoleLoaded: boolean = false;
  role: IRole;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private itemsService: ItemsService) { }  

    loadRoleForUpdate(id: number) {
        this.dataService.getRole(id)
          .subscribe((role: IRole) => {
              this.role = this.itemsService.getSerialized<IRole>(role);
              this.selectedRoleLoaded = true;
              this.loadRole.emit({});
          },
          error => {
              this.notificationService.printErrorMessage('Failed to load Role. ' + error);
          });
    }
  
    updateRole(editRoleForm: NgForm) {
        this.role = this.itemsService.getSerialized<IRole>(this.role);
        this.dataService.updateRole(this.role)
            .subscribe(() => {
                this.notificationService.printSuccessMessage('Role has been updated');
                this.roleUpdated.emit({});
                this.closeModal();
            },
            error => {
                this.notificationService.printErrorMessage('Failed to update Role. ' + error);
            });
        }

  openModal(id: number){
    this.loadRoleForUpdate(id);  
    this.editRoleModal.show();
  }

  closeModal() {
    this.editRoleModal.hide();
  }
}