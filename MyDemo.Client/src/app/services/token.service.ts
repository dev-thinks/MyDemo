import { Injectable, OnDestroy } from '@angular/core';
import { Token } from '../models/token';
import { LocalStorageService } from './local-storage.service';
import { JwtToken } from '../core/jwt-token';
import { currentTimestamp, filterObject } from '../core/util';


@Injectable({
  providedIn: 'root',
})
export class TokenService implements OnDestroy {
  private key = 'MyDemo-token';

  private _token?: JwtToken;

  constructor(private store: LocalStorageService) {}

  private get token(): JwtToken | undefined {
    if (!this._token) {
      this._token = new JwtToken(this.store.get(this.key));
    }

    return this._token;
  }

  set(token?: Token): TokenService {
    this.save(token);

    return this;
  }

  clear(): void {
    this.save();
  }

  valid(): boolean {
    return this.token?.valid() ?? false;
  }

  getUserid(): string {
    return this.token?.user_id ?? '';
  }

  getBearerToken(): string {
    return this.token?.getBearerToken() ?? '';
  }


  ngOnDestroy(): void {
  }

  /**
   * Save the token to local storage
   * @param token token model
   */
  private save(token?: Token): void {
    
    this._token = undefined;

    if (!token) {
      this.store.remove(this.key);
    } else {
      const value = Object.assign({ access_token: '', token_type: 'Bearer' }, token, {
        exp: token.expires_in ? currentTimestamp() + token.expires_in : null,
      });
      this.store.set(this.key, filterObject(value));
    }
  }
}
