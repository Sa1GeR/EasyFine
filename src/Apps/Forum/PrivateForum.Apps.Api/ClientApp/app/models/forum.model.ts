import { TopicModel } from "./topic.model";

export class ForumModel {
  public id: number;
  public name: string;
  public parentId: number = null;
  public isDeleted: boolean;

  public topics: TopicModel[];
  public subForums: ForumModel[];

  public deWay: ForumModel[];

  public created?: string;
  public createdBy?: string;
  public modified?: string;
  public modifiedBy?: string;
}