import { Component } from '@angular/core';
import { IRolesType, IPermissionType } from '../../model/RolesType';
import { UsersTypeDataService } from '../../services/userstype.dataservice';
import { UsersTypeView } from '../../model/UsersType';
import { FormBuilder, FormGroup, Validator } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
	selector: 'view-user',
	templateUrl: './viewuser.component.html',
	styleUrls: ['./viewuser.component.css'],

	
})


export class ViewUserComponent {

	public usersType: UsersTypeView[];

	constructor(private userDataService: UsersTypeDataService, private router: Router) {
		this.userType();
	}

	public editUsers(userId: number) {

		if (userId && userId > 0) {

			this.router.navigate(['/edit-user/' + userId]);
		} 
	}

	private deleteUsers(userId: number) {

		if (userId && userId > 0) {
			let message = window.confirm("Are you sure, you want to delete this record ?");
			if (message == true) {

				this.userDataService.removeUser(userId).subscribe(result => {

					if (result.ok) {
						alert("recor delete successful");
						this.userType();
					}
				}, error => console.error(error));


		
			}
		}
	}

	private userType() {
		this.userDataService.getAllUsersType()
			.subscribe(result => {
				this.usersType = result.json() as UsersTypeView[];
				console.log(this.usersType);
			}, error => console.error(error));

	}

}
