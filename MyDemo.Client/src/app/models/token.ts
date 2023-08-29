export interface Token {
  [prop: string]: any;

  //the Jwt token return from API after login successfully
  access_token: string;
  //the current user id
  user_id?: string;
  //should be just handle the 'Bearer' type in this sample
  token_type?: string;
  //How long will be the token expired(e.g. after 30 mins). This is a timestamp format
  expires_in?: number;
  //the actually expire time, so should be the expires_in + current time, e.g.
  //if expires_in = 30 mins, then exp would be current time + 30 mins
  exp?: number;
}
