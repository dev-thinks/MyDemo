
import { Injectable, OnDestroy } from "@angular/core";
import { Subject } from "rxjs";
import { SweetAlertOptions } from "sweetalert2";

//reference: https://github.com/sweetalert2/ngx-sweetalert2

@Injectable({
  providedIn: 'root',
})
export class SwalService implements OnDestroy {

  private swalSource = new Subject<SweetAlertOptions>();
  swalEmitted$ = this.swalSource.asObservable();

  private swalCloseSource = new Subject<boolean>();
  swalCloseEmitted$ = this.swalCloseSource.asObservable();

  private swalConfirmEvent = new Subject<SwalConfirmItem>();
  swalConfirmEmitted$ = this.swalConfirmEvent.asObservable();


  // Show swal with options
  // icon: SweetAlertIcon = 'success' | 'error' | 'warning' | 'info' | 'question'
  show(options: SweetAlertOptions) {
    this.swalSource.next(options);
  }

  // Close the swal
  close() {
    this.swalCloseSource.next(true);
  }


  // Handle the HttpErrorResponse and show the errors box
  showErrors(error: any, options: SweetAlertOptions) {
    console.log('%c [ error ]-37', 'font-size:13px; background:pink; color:#bf2c9f;', error);

    if (error.error && error.error.errors) {
      var errors = '';
      for (var key in error.error.errors) {
        errors += error.error.errors[key] + '<br>';
      }
      options.html = error.error.title + '<br>' + errors;
    } else {
      options.html = error.error;
    }

    options.icon = 'error';
    this.swalSource.next(options);
  }

  // Set the confirm event
  setConfirm(confirmItem: SwalConfirmItem) {
    this.swalConfirmEvent.next(confirmItem);
  }

  ngOnDestroy() {
    // complete and release the subject
    this.swalSource.complete();
    this.swalCloseSource.complete();
    this.swalConfirmEvent.complete();
  }
}

export interface SwalConfirmItem {
  //the confirm handler function
  fnConfirm: any;
  //the data need to be pass to the confirm function
  confirmData: any;
  //the current context of the component
  context: any;
}
