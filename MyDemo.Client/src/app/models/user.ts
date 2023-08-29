/**
 * ~ User model for mapping to Api
 */
export interface User {
  id?: number;
  isActive?: boolean;
  isDeleted?: boolean;
  name?: string;
  password?: string;
  email?: string;
  createdAt?: Date;
  updatedAt?: Date;
}
