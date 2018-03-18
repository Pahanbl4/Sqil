import { IPermissionDetails, IRole, IUser, IUserDetails, IUserEdit } from './../interfaces';
import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
//Grab everything with import 'rxjs/Rx';
import { Observable } from 'rxjs/Observable';
import { Observer } from 'rxjs/Observer';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { IPermission, Pagination, PaginatedResult } from '../interfaces';
import { ItemsService } from '../utils/items.service';
import { ConfigService } from '../utils/config.service';

@Injectable()
export class DataService {

    _baseUrl: string = '';

    constructor(private http: Http,
                private itemsService: ItemsService,
                private configService: ConfigService) 
    {
        this._baseUrl = configService.getApiURI();
    }

   

    // Permissions
    getPermissions(page?: number, itemsPerPage?: number): Observable<PaginatedResult<IPermission[]>> {
        var peginatedResult: PaginatedResult<IPermission[]> = new PaginatedResult<IPermission[]>();

        let headers = new Headers();
        if (page != null && itemsPerPage != null) {
            headers.append('Pagination', page + ',' + itemsPerPage);
        }

        return this.http.get(this._baseUrl + 'permissions', {
            headers: headers
        })
            .map((res: Response) => {
                console.log(res.headers.keys());
                peginatedResult.result = res.json();

                if (res.headers.get("Pagination") != null) {
                    //var pagination = JSON.parse(res.headers.get("Pagination"));
                    var paginationHeader: Pagination = this.itemsService.getSerialized<Pagination>(JSON.parse(res.headers.get("Pagination")));
                    console.log(paginationHeader);
                        peginatedResult.pagination = paginationHeader;
                }
                return peginatedResult;
            })
            .catch(this.handleError);
    }

    getPermissionDetails(id: number): Observable<IPermissionDetails> {
        return this.http.get(this._baseUrl + 'permissions/' + id)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }   

    createPermission(permission: IPermission): Observable<IPermission> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.post(this._baseUrl + 'permissions/', JSON.stringify(permission), {
            headers: headers
        })
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }

    getSchedule(id: number): Observable<IPermission> {
        return this.http.get(this._baseUrl + 'schedules/' + id)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    } 

    updatePermission(permission: IPermission): Observable<void> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.put(this._baseUrl + 'permissions/' + permission.id, JSON.stringify(permission), {
            headers: headers
        })
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    deletePermission(id: number): Observable<void> {
        return this.http.delete(this._baseUrl + 'permissions/' + id)
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    // Roles
    getRoles(page?: number, itemsPerPage?: number): Observable<PaginatedResult<IRole[]>> {
        var peginatedResult: PaginatedResult<IRole[]> = new PaginatedResult<IRole[]>();

        let headers = new Headers();
        if (page != null && itemsPerPage != null) {
            headers.append('Pagination', page + ',' + itemsPerPage);
        }

        return this.http.get(this._baseUrl + 'roles', {
            headers: headers
        })
            .map((res: Response) => {
                peginatedResult.result = res.json();

                if (res.headers.get("Pagination") != null) {
                    //var pagination = JSON.parse(res.headers.get("Pagination"));
                    var paginationHeader: Pagination = this.itemsService.getSerialized<Pagination>(JSON.parse(res.headers.get("Pagination")));                  
                    peginatedResult.pagination = paginationHeader;
                }
                return peginatedResult;
            })
            .catch(this.handleError);
    }

    getAllRoles(): Observable<IRole[]> {
        return this.http.get(this._baseUrl + 'roles/all')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    } 

    getRole(id: number): Observable<IRole> {
        return this.http.get(this._baseUrl + 'roles/' + id)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }   

    createRole(role: IRole): Observable<IRole> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.post(this._baseUrl + 'roles/', JSON.stringify(role), {
            headers: headers
        })
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }

    updateRole(role: IRole): Observable<void> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.put(this._baseUrl + 'roles/' + role.roleID, JSON.stringify(role), {
            headers: headers
        })
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    deleteRole(id: number): Observable<void> {
        return this.http.delete(this._baseUrl + 'roles/' + id)
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

   

    //Users
    getUsers(page?: number, itemsPerPage?: number): Observable<PaginatedResult<IUser[]>> {
        var peginatedResult: PaginatedResult<IUser[]> = new PaginatedResult<IUser[]>();

        let headers = new Headers();
        if (page != null && itemsPerPage != null) {
            headers.append('Pagination', page + ',' + itemsPerPage);
        }

        return this.http.get(this._baseUrl + 'users', {
            headers: headers
        })
            .map((res: Response) => {
                peginatedResult.result = res.json();

                if (res.headers.get("Pagination") != null) {
                    //var pagination = JSON.parse(res.headers.get("Pagination"));
                    var paginationHeader: Pagination = this.itemsService.getSerialized<Pagination>(JSON.parse(res.headers.get("Pagination")));                   
                        peginatedResult.pagination = paginationHeader;
                }
                return peginatedResult;
            })
            .catch(this.handleError);
    }

    createUser(user: IUser): Observable<IUser> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.post(this._baseUrl + 'users/', JSON.stringify(user), {
            headers: headers
        })
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }

    getUserRoleDetails(id: number): Observable<IUserDetails> {
        return this.http.get(this._baseUrl + 'users/' + id + '/roles')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }   

    getUserRolePermissionDetails(id: number): Observable<IUserDetails> {       
        return this.http.get(this._baseUrl + 'users/' + id + '/permissions')
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    }  

    getUser(id: number): Observable<IUserEdit> {
        return this.http.get(this._baseUrl + 'users/' + id)
            .map((res: Response) => {
                return res.json();
            })
            .catch(this.handleError);
    } 

    updateUser(user: IUser): Observable<void> {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.put(this._baseUrl + 'users/' + user.id, JSON.stringify(user), {
            headers: headers
        })
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    deleteUser(id: number): Observable<void> {
        return this.http.delete(this._baseUrl + 'users/' + id)
            .map((res: Response) => {
                return;
            })
            .catch(this.handleError);
    }

    private handleError(error: any) {
        var applicationError = error.headers.get('Application-Error');
        var serverError = error.json();
        var modelStateErrors: string = '';

        if (!serverError.type) {
            console.log(serverError);
            for (var key in serverError) {
                if (serverError[key])
                    modelStateErrors += serverError[key] + '\n';
            }
        }

        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;

        return Observable.throw(applicationError || modelStateErrors || 'Server error');
    }
}