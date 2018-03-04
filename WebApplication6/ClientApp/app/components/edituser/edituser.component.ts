import { Component, OnInit } from '@angular/core';
import { IRolesType, IPermissionType } from '../../model/RolesType';
import { UsersTypeDataService } from '../../services/userstype.dataservice';
import { UsersType } from '../../model/UsersType';
import { FormBuilder, FormGroup, Validator } from '@angular/forms';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({

	selector: 'edit-user',
	templateUrl: './edituser.component.html',
	styleUrls: ['./edituser.component.css']
})


export class EditUserComponent implements OnInit {
	
	private RolesType: IRolesType[];
	private PermissionType: IPermissionType[];
	private Users: UsersType = this.initializeUsersType();
	private userId: number;

	ngOnInit(): void {
		this.activatedRoute.params.subscribe((params: Params) => {
			this.userId = params["userId"];		
			this.dataservice.getUserTypeById(this.userId)
				.subscribe(result =>
				{
					this.Users = result.json() as UsersType;
					console.log(this.Users)
				}, error =>
				{
					console.log(error);
				});
		})

	}

	private initializeUsersType() {
		return new UsersType(0, "", "", 0, 0,0);
	}
	constructor(
		private dataservice: UsersTypeDataService,
		private router: Router,
		private activatedRoute: ActivatedRoute
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

	

		if (this.Users && this.Users.name && this.Users.name.length > 0 && this.userId > 0) {
			this.dataservice.editUsers(this.userId, this.Users)
				.subscribe(resp => {
					if (resp.ok) {

						var userId = resp.json().id;
						alert("User Edit Successfuly witch jobId: " + userId);
						this.router.navigate(['/add-user']);
					}
				}, error => {
					console.log(error)
				});
		}
	}
}
