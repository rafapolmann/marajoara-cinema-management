export interface UserAccount {
  userAccountID: number,
  name: string,
  mail: string,
  level: number,
  photo?: string,
  photoFile?: File,
}

export interface AuthorizedUserAccount {
  userAccountID: number,
  name: string,
  level: AccessLevel,
  mail: string,
  token: string
}

export interface UserAccountChangePassword {
  userAccountID: number,
  mail: string,
  password: string,
  newPassword: string,
}

export enum AccessLevel {
  manager = 1,
  attendant = 2,
  customer = 3,
}
