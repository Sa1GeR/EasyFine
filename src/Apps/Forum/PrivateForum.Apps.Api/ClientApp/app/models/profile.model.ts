export class ProfileModel {
  public id: number;
  public firstName: string;
  public middleName: string;
  public lastName: string;
  public email: string;
  public address: string;
  public avatarUrl?: string;
  public isBlocked: boolean;
}