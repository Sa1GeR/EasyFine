import { MessageModel } from "./message.model";

export class TopicModel {
  public id: number;
  public header: string;
  public subtitle?: string;
  public folderId: number;
  public headId: number;
  public isDeleted: boolean;

  public messages: MessageModel[];

  public created?: string;
  public createdBy?: string;
  public modified?: string;
  public modifiedBy?: string;
}