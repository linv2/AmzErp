/**
 * @description: Login interface parameters
 */
export interface LoginParams {
  userName: string;
  passWord: string;
}

export interface RoleInfo {
  roleName: string;
  value: string;
}

/**
 * @description: Login interface return value
 */
export interface LoginResultModel {
  userId: string | number;
  token: string;
  role: RoleInfo;
}

/**
 * @description: Get user information return value
 */
export interface GetUserInfoModel {
  id: number,
  userName: string
  displayName: string
  userType: number,
  lastLoginTime: string,
  lastLoginIP: string,
  loginCount: number,
  disable: boolean
}