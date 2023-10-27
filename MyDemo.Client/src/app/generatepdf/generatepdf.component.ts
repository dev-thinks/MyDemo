import { Component, ViewChild } from '@angular/core';
import { SwalService } from '../services/swal.service';
import { LoadingService } from '../services/loading.service';
import { SweetAlertOptions } from 'sweetalert2';
import { GeneratePDFService } from '../services/generate-pdf.service';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { ngxLoadingAnimationTypes } from 'ngx-loading';

const PrimaryRed = '#dd0031';
const SecondaryBlue = '#1976d2';

@Component({
  selector: 'app-generatepdf',
  templateUrl: './generatepdf.component.html',
  styleUrls: ['./generatepdf.component.scss']
})
export class GeneratepdfComponent {

  isSwalVisible = false;
  swalConfirmData: any;
  swalComponentContext: any;

  @ViewChild(SwalComponent) swalBox!: SwalComponent;
  public swalOptions: SweetAlertOptions = { icon: 'info' };

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

  constructor(
    private swalService: SwalService,
    private generatePDFService: GeneratePDFService,
    private loadingService: LoadingService) {
    //fire the swal from child component
    this.swalService.swalEmitted$.subscribe(options => {
      if (!this.swalBox) {
        //just wait for the time to load the object if can't find it
        setTimeout(() => {
          if (this.swalBox) {
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
      this.swalConfirmData = confirmItem.confirmData;
      this.swalComponentContext = confirmItem.context;
    });

    //handle close the swal event
    this.swalService.swalCloseEmitted$.subscribe(item => {
      this.swalBox.close();
    });
  }

  public generate() {
    this.loadingService.start();
    this.generatePDFService.generatePDF().subscribe(blob => {

      if (blob) {
        var downloadURL = window.URL.createObjectURL(new Blob([blob], { type: 'blob' }));
        var link = document.createElement('a');
        link.href = downloadURL;
        link.download = "Demo.pdf";
        link.click();
        this.swalOptions.icon = 'success';
        this.swalOptions.title = 'Generate PDF';
        this.swalOptions.html = `The PDF has been downloaded!`;
        this.swalOptions.showConfirmButton = true;
        this.swalOptions.showCancelButton = false;
        this.swalService.show(this.swalOptions);
      }
      else {
        this.swalOptions.icon = 'error';
        this.swalOptions.title = 'Generate Appendix';
        this.swalOptions.html = `Failed to generate PDF, please find the error in server side!`;
        this.swalOptions.showConfirmButton = true;
        this.swalOptions.showCancelButton = false;
        this.swalService.show(this.swalOptions);
      }

      this.loadingService.stop();

    }, error => {
      console.log('%c [ error ]-52', 'font-size:13px; background:pink; color:#bf2c9f;', error);
      this.loadingService.stop();
    });
  }
}
