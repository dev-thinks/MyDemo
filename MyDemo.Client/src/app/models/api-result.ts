
/**
 * ~ The common API result model
 */
export interface ApiResult<T> {
  data?: T;
  success: boolean;
  message: string;
}
