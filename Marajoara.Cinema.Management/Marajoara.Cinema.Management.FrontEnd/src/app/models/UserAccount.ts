export interface UserAccount {
  userAccountID: number,
  name: string,
  mail: string,
  level: number
}
export interface AuthorizedUserAccount{  
    userAccountID:Number,
    name:string,
    level:AccessLevel,
    mail:string,
    token:string  
}

enum AccessLevel{
  manager=1,
  attendant = 2,
  customer = 3,
}
