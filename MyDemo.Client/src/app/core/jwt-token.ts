import { Token } from "../models/token";
import { capitalize, currentTimestamp } from "./util";

export class JwtToken {
  constructor(protected attributes: Token) {}

  get access_token(): string {
    return this.attributes.access_token;
  }

  get user_id(): string {
    return this.attributes.user_id ?? '';
  }

  get token_type(): string {
    return this.attributes.token_type ?? 'bearer';
  }

  get exp(): number | void {
    return this.attributes.exp;
  }

  valid(): boolean {
    return this.hasAccessToken() && !this.isExpired();
  }

  getBearerToken(): string {
    return this.access_token
      ? [capitalize(this.token_type), this.access_token].join(' ').trim()
      : '';
  }

  private hasAccessToken(): boolean {
    return !!this.access_token;
  }

  /**
  Check the expired time
  Unit: seconds
  */
  private isExpired(): boolean {
    return this.exp !== undefined && this.exp - currentTimestamp() <= 0;
  }

}
