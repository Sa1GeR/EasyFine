export class UserModel {
  public id: number;
  public role: string;
}

export enum Roles {
  Administrator = "Administrator",
  Client = "Client"
}