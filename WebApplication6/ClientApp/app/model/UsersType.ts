export class UsersType {

	constructor(
		public id: number,
		public name: string,
		public email: string,
		public password: number,
		public rolesId: number,
		public permissionId: number


	) {}
}

export class UsersTypeView {

	constructor(
		public usersId: number,
		public userName: string,
		public email: string,
		public password: number,
		public rolesName: string,
		public permissionName: string


	) { }
}
 