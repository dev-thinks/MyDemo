import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { debounceTime, Observable, tap } from 'rxjs';
import { config } from 'src/assets/config';
import { DataListResult } from '../models/data-list-result';
import { ApiResult } from '../models/api-result';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class UserManagementService {

  constructor(public http: HttpClient) { }

  getUrl(url: string) {
      console.log(config.apiUrl + url);
      return config.apiUrl + url;
      //return '/api' + url;
  }

  public getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string | null,
    filterQuery: string | null
  ): Observable<ApiResult<DataListResult<User>>> {
    var url = this.getUrl("/users");
    var params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);
    if (filterColumn && filterQuery) {
      params = params
        .set("filterColumn", filterColumn)
        .set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult<DataListResult<User>>>(url, { params });
  }

}
