import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MtxGridColumn } from '@ng-matero/extensions/grid';
import { UserManagementService } from '../services/user-management.service';
import { MtxDialog } from '@ng-matero/extensions/dialog';
import { Sort } from '@angular/material/sort';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss']
})
export class UserManagementComponent implements OnInit {

  constructor(private userManagementService: UserManagementService,
    private dialog: MtxDialog, private auth: AuthService) { }


  public defaultFilterColumn: string = "name";
  public defaultPageIndex: number = 0;
  public defaultPageSize: number = 10;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: "asc" | "desc" = "asc";
  public filterQuery?: string;
  public isLoading = true;
  public list: any[] = [];
  public total = 0;

  public columns: MtxGridColumn[] = [
    // {
    //   header: 'Operation', field: 'operation' ,
    //   minWidth: 140,
    //   //width: '140px',
    //   pinned: 'left',
    //   type: 'button',
    //   buttons: [
    //     {
    //       type: 'icon',
    //       icon: 'edit',
    //       tooltip: 'Edit',
    //       click: record => this.edit(record),
    //     },
    //     {
    //       color: 'warn',
    //       icon: 'delete',
    //       text: 'Delete',
    //       tooltip: 'Delete',
    //       pop: {
    //         title: 'Do you confirm to delete the user ?',
    //         closeText: 'Close',
    //         okText: 'Ok',
    //       },
    //       click: record => this.delete(record),
    //     },
    //   ],
    // },
    { header: 'Id', field: 'id', hide: true },
    { header: 'Active', field: 'isActive', sortable: true,
      type: 'tag',
      tag: {
        true: { text: 'Yes', color: 'red-100' },
        false: { text: 'No', color: 'green-100' },
      },
    },
    { header: 'User Name', field: 'name', sortable: true, minWidth: 250 },
    { header: 'Email', field: 'email', minWidth: 250 ,sortable: true },
    { header: 'Created Date', field: 'createdAt' , minWidth: 150, sortable: true},
    { header: 'Updated Date', field: 'updatedAt' , minWidth: 160, sortable: true},
  ];

  public query = {
    q: '',
    sort: 'name',
    order: 'desc',
    page: 0,
    per_page: this.defaultPageSize,
  };

  public ngOnInit() {
    this.loadData();
  }

  public search() {
    this.query.page = 0;
    this.getData();
  }

  public sortingChange(e: Sort) {
    //console.log('sortingChange', e);
    this.query.sort = e.active;
    this.query.order = e.direction;
    this.getData();
  }

  public reset() {
    this.query.q = '';
    this.query.page = 0;
    this.getData();
  }

  public logout() {
    this.auth.logout();
  }

  batchDelete() {
    throw new Error('Method not implemented.');
  }
  create() {
    throw new Error('Method not implemented.');
  }

  delete(record: any): void {
    throw new Error('Method not implemented.');
  }
  edit(record: any): void {
    throw new Error('Method not implemented.');
  }

  public getData() {

    var sortColumn = (this.query.sort)
      ? this.query.sort
      : this.defaultSortColumn;

    var order = (this.query.order)
      ? this.query.order
      : this.defaultSortOrder;

    var filterQuery = (this.query.q)
      ? this.query.q
      : null;

    this.userManagementService.getData(
      this.query.page,
      this.query.per_page,
      sortColumn,
      order,
      this.defaultFilterColumn,
      filterQuery).subscribe(res => {
          //console.log('res', res);
          if(res.success){
            this.list = res.data ? res.data.data : [] ;
            this.total = res.data? res.data.totalCount : 0;
          } else {
            this.dialog.alert(`Failed to get data!`);
          }
        }
      );
  }

  public loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData();
  }

  public getNextPage(e: PageEvent) {
    this.query.page = e.pageIndex;
    this.query.per_page = e.pageSize;
    this.getData();
  }

}
