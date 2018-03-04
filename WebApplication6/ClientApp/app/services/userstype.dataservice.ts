import { Http,Headers,RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';
import { UsersType } from '../model/UsersType';


@Injectable()
export class UsersTypeDataService {

	private baseUrl: string = "http://localhost:59693/api/";

	constructor(private http: Http) {

	}

	public saveUsers(usersType: UsersType) {
		let header = new Headers({ 'Content-Type': 'application/json' });
		let options = new RequestOptions({ headers: header });
		return this.http.post(this.baseUrl + "Users", usersType, options);
	}
	

	public removeUser(userId: number) {
		return this.http.delete(this.baseUrl + "Users/" + userId);
	}

	public getAllRolesTypes() {
		return this.http.get(this.baseUrl +"Roles")
	}

	public getAllPermissionTypes() {
		return this.http.get(this.baseUrl + "Permissions")
	}

	public getAllUsersType() {
		return this.http.get(this.baseUrl + "Users")
	}

	public getUserTypeById(userId: number) {
		return this.http.get(this.baseUrl + "Users/" + userId);
	}

	public editUsers(userId: number, Users: UsersType) {

		let header = new Headers({ 'Content-Type': 'application/json' });
		let options = new RequestOptions({ headers: header });

		let url = this.baseUrl + "Users/" + userId;
		return this.http.put(url, Users, options);

	}
}