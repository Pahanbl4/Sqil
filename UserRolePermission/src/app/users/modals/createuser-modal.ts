import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IUser } from './../../shared/interfaces';
import { Component, OnInit, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'createUser-modal',
  template: `
  <div bsModal #createUserModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                  <span aria-hidden="true">&times;</span>
              </button>
              <h4>NEW User</h4>
          </div>
          <div class="modal-body">
              <form #createUserForm="ngForm" novalidate>
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
                                         [(ngModel)]="user.firstMidName" 
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
                                         [(ngModel)]="user.lastName" 
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
                                  <datepicker [(ngModel)]="user.hireDate" name="timeStartDate" [showWeeks]="false"></datepicker>
                              </div>
                             
                          </div>
                      </div>  
                      <div class="row">
                          <div class="col-md-12">  
                              <label class="control-label">Roles</label>  
                              <br />                                         
                              <div *ngFor="let role of user.roles" class="col-md-4">
                                  <input type="checkbox"
                                         [id]="role.roleID"                                          
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
                          [disabled]="!createUserForm.form.valid"
                          (click)="addUser(createUserForm)">Submit
                  </button>
              </form>  
          </div>
      </div>
  </div>
</div>
  `,
})

export class CreateUserModalComponent {
  @ViewChild('createUserModal') public createUserModal: ModalDirective;
  @Input() user: IUser;
  @Output() userCreated = new EventEmitter();
  selectedRoles: string[] = []; // Initialize

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService) { }  
      
    addUser(form: NgForm) {
        // Find checkbox(es) that are checked and add to an array
        this.user.roles.forEach(item => {
        if (item.assigned)
            this.selectedRoles.push(item.roleID.toString());
        });
        this.user.selectedRoles = this.selectedRoles;
        
        this.dataService.createUser(this.user)
            .subscribe((userCreated) => {
                this.notificationService.printSuccessMessage(userCreated.firstMidName + ' ' + userCreated.lastName + ' has been created');
                this.userCreated.emit({});
                this.closeModal();                
            },
            error => {
                this.notificationService.printErrorMessage('Failed to create a new user. ' + error);
            })
    
        this.selectedRoles = []; // Reset, so it will not add up the previously selected courses
        form.reset();
      }
      
  openModal(){
    this.createUserModal.show();
  }

  closeModal() {
    this.createUserModal.hide();
  }
}