import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormBuilder } from '@angular/forms';
import { filter } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { SwalService } from '../services/swal.service';
import { SweetAlertOptions } from 'sweetalert2';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  constructor(private fb: FormBuilder,
    private router: Router,
    private auth: AuthService,
    private swalService: SwalService,
    private loadingService: LoadingService) {
  }

  swalOptions: SweetAlertOptions = { icon : 'info' };

  public errorMsg: string = '';

  loginForm = this.fb.nonNullable.group({
    username: '',
    password: '',
  });

  get username() {
    return this.loginForm.get('username')!;
  }

  get password() {
    return this.loginForm.get('password')!;
  }

  login() {
    console.log('user name:' + this.username.value);

    this.loadingService.start();

    this.auth
      .login(this.username.value, this.password.value)
      .pipe(filter(authenticated => authenticated))
      .subscribe(
        () => {
          console.log('after logined and redirect');

          this.loadingService.stop();

          this.router.navigateByUrl('/user-management');
        },
        (errorRes: HttpErrorResponse) => {

          this.loadingService.stop();

          if(errorRes.status == 401){
            // this.errorMsg = 'User name or password is not valid!';
            // set the swal icon to 'error'
            this.swalOptions.icon = 'error';
            // set the message need to be show
            this.swalOptions.html =  'User name or password is not valid!';
            // show the swal box
            this.swalService.show(this.swalOptions);
          }
            console.log('Error', errorRes);
        }
      );
  }

}
