import { Component, OnInit } from '@angular/core';
import { IRolesType, IPermissionType } from '../../model/RolesType';
import { UsersTypeDataService } from '../../services/userstype.dataservice';
import { UsersType } from '../../model/UsersType';
import { FormBuilder, FormGroup, Validator } from '@angular/forms';
import { Router } from '@angular/router';


@Component({
	selector: 'add-user',
	templateUrl: './adduser.component.html',
	styleUrls: ['./adduser.component.css'],

	providers: [UsersTypeDataService]
})


export class AddUserComponent {

	private RolesType: IRolesType[];
	private PermissionType: IPermissionType[];
	private Users: UsersType = this.initializeUsersType();


	private initializeUsersType() {
		return new UsersType(0,"", "", 0, 0, 0);
	}
	constructor(
		private dataservice: UsersTypeDataService,
		private router: Router
	) {


		this.dataservice.getAllRolesTypes()
			.subscribe(result => {
				this.RolesType = result.json() as IRolesType[];
				console.log(this.RolesType);
			}, error => console.error(error));

		this.dataservice.getAllPermissionTypes()
			.subscribe(result => {
				this.PermissionType = result.json() as IPermissionType[];
				console.log(this.PermissionType);
			}, error => console.error(error));
	}

	onSubmit() {

		console.log(this.Users);
	
		if (this.Users && this.Users.name && this.Users.name.length > 0) {
			this.dataservice.saveUsers(this.Users)
				.subscribe(resp => {
					if (resp.ok) {

						var userId = resp.json().id;
						alert("User saved Successfuly witch jobId: " + userId);
						this.router.navigate(['/view-user']);
					}
				}, error => {
					console.log(error)
				});
		}
	}
}
