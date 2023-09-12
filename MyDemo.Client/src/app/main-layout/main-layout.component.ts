import { Component, OnDestroy, ViewChild } from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { SweetAlertOptions } from 'sweetalert2';
import { SwalConfirmItem, SwalService } from '../services/swal.service';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { LoadingService } from '../services/loading.service';


const PrimaryRed = '#dd0031';
const SecondaryBlue = '#1976d2';

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

  //set the loading flag
  loading = false;
  //set the loading config
  //reference:  https://github.com/Zak-C/ngx-loading#config-options
  config = {
    animationType: ngxLoadingAnimationTypes.threeBounce,
    primaryColour: PrimaryRed,
    secondaryColour: SecondaryBlue,
    tertiaryColour: PrimaryRed,
    backdropBorderRadius: '3px',
  };

  constructor(private swalService: SwalService, private loadingService: LoadingService){
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

    // Update loading status
    this.loadingService.changeEmitted$.subscribe(isLoading => {
      //console.log(isLoading);
      this.loading = isLoading;
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
