export class CommonModel {
}



export class Dictionary<T> implements IDictionary<T>
{
  private items: { [index: string]: T } = {};

  private count: number = 0;

  public ContainsKey(key: string): boolean {
    return this.items.hasOwnProperty(key);
  }

  public Count(): number {
    return this.count;
  }

  public Add(key: string, value: T) {
    if (!this.items.hasOwnProperty(key))
      this.count++;
    this.items[key] = value;
  }

  public Remove(key: string): T {
    var val = this.items[key];
    delete this.items[key];
    this.count--;
    return val;
  }

  public Item(key: string): T {
    return this.items[key];
  }

  public Keys(): string[] {
    var keySet: string[] = [];
    for (var prop in this.items) {
      if (this.items.hasOwnProperty(prop)) {
        keySet.push(prop);
      }
    }
    return keySet;
  }

  public Values(): T[] {
    var values: T[] = [];
    for (var prop in this.items) {
      if (this.items.hasOwnProperty(prop)) {
        values.push(this.items[prop]);
      }
    }
    return values;
  }
}

export interface IDictionary<T> {
  Add(key: string, value: T): any;
  ContainsKey(key: string): boolean;
  Count(): number;
  Item(key: string): T;
  Keys(): string[];
  Remove(key: string): T;
  Values(): T[];
}

export class ApiResponse<T> {
  IsSuccess!: boolean;
  Message!: string | null;
  StatusCode!: number;
  Data!: T | null;
  Exception!: string | null;
  TotalRecord?: number;
}

export class IndexModel {
  Page: number;
  PageSize: number;
  Search!: string;
  OrderBy!: string;
  OrderByAsc: boolean;
  IsPostBack: boolean;
  AdvanceSearchModel?: any;
  constructor() {
    this.PageSize = 10;
    this.IsPostBack = false;
    this.OrderByAsc = true;
    this.Page = 1;
  }
}

export class DropDownModel {
  ddlParentUserRole!: DropDownItem[];
  ddlUserRole!: DropDownItem[];
  ddlState!: DropDownItem[];
  ddlDistrict!: DropDownItem[];
  ddlGender!: DropDownItem[];
  ddlPaymentMode!: DropDownItem[];
  ddlLookupTypeMaster!: DropDownItem[];
}

export class DropDownItem {
  Text: string = "";
  Value: string = "";
}
export interface FilterDropDownPostModel {
  Key: string;
  FileterFromKey: string;
  Values: number[];
}
