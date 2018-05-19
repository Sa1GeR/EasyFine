export class MessageModel {
  public id: number;
  public content: string;
  public replyId: number = null;
  public topicId: number;
  public isDeleted: boolean;
  
  public authorId: number;
  public avatarUrl: string;
  public firstName: string;
  public lastName: string;

  public created?: string;
  public createdBy?: number;
  public modified?: string;
  public modifiedBy?: number;
}
