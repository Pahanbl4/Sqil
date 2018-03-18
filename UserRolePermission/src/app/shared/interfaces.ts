

export interface IPermission {
    id: number;
    permissionName: string;

}

export interface IPermissionDetails {
    id: number;
    permissionName: string;
    enrollments: IEnrollment[];
}

export interface IEnrollment {
    title: string;
    fullName: string;
}

export interface IRole {
    roleID: number;
    title: string;
    assigned: boolean;
}


export interface IUser {
    id: number;
    lastName: string;
    firstMidName: string;
    hireDate: Date;
    selectedRoles: string[];
    roles: IRole[];
}

export interface IUserDetails {
    id: number;
    roles: IRole[];
    enrollments: IEnrollment[];
}

export interface IUserEdit {
    id: number;
    lastName: string;
    firstMidName: string;
    hireDate: Date;
    office: string;  
    assignedRoles: IRole[];
}

export interface Pagination {
    CurrentPage : number;
    ItemsPerPage : number;
    TotalItems : number;
    TotalPages: number;
}

export class PaginatedResult<T> {
    result :  T;
    pagination : Pagination;
}

export interface Predicate<T> {
    (item: T): boolean
}