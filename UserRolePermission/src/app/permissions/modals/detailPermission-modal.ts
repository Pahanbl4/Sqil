import { DateFormatPipe } from './../../shared/pipes/date-format.pipe';
import { ItemsService } from './../../shared/utils/items.service';
import { NotificationService } from './../../shared/utils/notification.service';
import { DataService } from './../../shared/services/data.service';
import { IPermission, IPermissionDetails } from './../../shared/interfaces';
import { Component, ViewChild, Input, EventEmitter, Output } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'detailPermission-modal',
  template: `
  <div bsModal #detailPermissionModal="bs-modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg" *ngIf="selectedPermissionLoaded">
      <div class="modal-content">
          <div class="modal-header">
              <button type="button" class="close" aria-label="Close" (click)="closeModal()">
                   <span aria-hidden="true">&times;</span>
              </button>
              <h4>Permission DETAILS</h4>
          </div>
          <div class="modal-body">
              <form ngNoForm method="post">
                  <div class="form-group">
                      <div class="row">
                          <div class="col-md-12">
                               <div class="col-md-6">
                                   <label class="control-label">Permission Name</label>
                                   <input type="text" class="form-control" [(ngModel)]="permissionDetails.permissionName" disabled />
                               </div>        
                               
                          </div>
                      </div>
                      <br />
                                          
                  </div>          
                  <hr/>
                  <div class="panel panel-info">
                      <div class="panel-heading">Enrollments</div>
                      <table class="table table-hover">
                          <thead>
                              <tr>                                 
                                  <th>Permission Title</th>
                                
                              </tr>
                          </thead>
                          <tbody>
                              <tr *ngFor="let enrollment of permissionDetails.enrollments">                                  
                                  <td>{{enrollment.title}}</td>
                               
                              </tr>
                          </tbody>
                      </table>
                  </div>
              </form>
          </div>
      </div>
  </div>
</div>
  `,
})

export class DetailPermissionModalComponent {    
  @ViewChild('detailPermissionModal') public detailPermissionModal: ModalDirective;
  permissionDetails: IPermissionDetails;    
  selectedPermissionLoaded: boolean = false;

  constructor(
    private dataService: DataService,
    private notificationService: NotificationService,
    private itemsService: ItemsService) { }  

   viewPermissionDetails(id: number) {
    this.dataService.getPermissionDetails(id)
        .subscribe((permission: IPermissionDetails) => {
            this.permissionDetails = this.itemsService.getSerialized<IPermissionDetails>(permission);
        
            this.selectedPermissionLoaded = true;
            this.detailPermissionModal.show();//.open('lg');
        },
        error => {
            this.notificationService.printErrorMessage('Failed to load Permission. ' + error);
        });
  }
  
  openModal(id: number){
    this.viewPermissionDetails(id);  
    this.detailPermissionModal.show();
  }

  closeModal() {
    this.detailPermissionModal.hide();
  }
}