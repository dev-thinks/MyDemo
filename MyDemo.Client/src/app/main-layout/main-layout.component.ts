import { Component, OnDestroy, ViewChild } from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { SweetAlertOptions } from 'sweetalert2';
import { SwalConfirmItem, SwalService } from '../services/swal.service';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent {

  isSwalVisible = false;
  swalConfirmData : any;
  swalComponentContext: any;

  @ViewChild(SwalComponent) swalBox!: SwalComponent;
  swalOptions: SweetAlertOptions = {};

  constructor(private swalService: SwalService){
    //fire the swal from child component
    this.swalService.swalEmitted$.subscribe(options => {
      if(!this.swalBox){
        //just wait for the time to load the object if can't find it
        setTimeout(()=>{
          if(this.swalBox){
            this.isSwalVisible = true;
            this.swalBox.update(options);
            this.swalBox.fire();
          }
        }, 500);
      }
      else {
        this.isSwalVisible = true;
        this.swalBox.update(options);
        this.swalBox.fire();
      }
    });

    //set the confirm function and execute the login in child component
    this.swalService.swalConfirmEmitted$.subscribe(confirmItem => {
      this.handleConfirm = confirmItem.fnConfirm == null ? this.resetHandleConfirm : confirmItem.fnConfirm;
      this.swalConfirmData = confirmItem.confirmData;
      this.swalComponentContext = confirmItem.context;
    });

    //handle close the swal event
    this.swalService.swalCloseEmitted$.subscribe(item => {
      this.swalBox.close();
    });
  }

  handleConfirm(item: string, data: any, context: any): void {
    //this will be overwrite by this.swalService.swalConfirmEmitted$
  }

  //just for reset(remove) the existing handle confirm function
  resetHandleConfirm(item: string, data: any, context: any): void {
    //this will be overwrite by this.swalService.swalConfirmEmitted$
  }

}
