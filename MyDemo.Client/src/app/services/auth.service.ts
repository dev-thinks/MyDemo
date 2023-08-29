import { Injectable } from '@angular/core';
import { map, tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { config } from 'src/assets/config';
import { Token } from '../models/token';
import { TokenService } from './token.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(
    private tokenService: TokenService, private router: Router,
    protected http: HttpClient) {}

    /**
     * Call the API to login
     * @param username user name
     * @param password password
     * @returns Jwt token if login successfully
     */
    login(username: string, password: string) {

      var url = config.apiUrl + "/auth/login";
      //call the API to get token after login successfully
      return this.http.post<Token>(url, { username, password }).pipe(
        tap(token => {
          console.log('auth service logined ', token);
          //save the token into local storage
          this.tokenService.set(token);
        }),
        map(() => {
          console.log('auth service logined and map ', this.check());
          //check the token whether is valid
          return this.check();
        })
      );
    }

    /**
     * Clear the token after logout
     */
    logout(){
      this.tokenService.clear();
      this.router.navigateByUrl('/login');
    }

    check() {
      return this.tokenService.valid();
    }

}
