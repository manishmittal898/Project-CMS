import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-general-entry-master-detail',
  templateUrl: './general-entry-master-detail.component.html',
  styleUrls: ['./general-entry-master-detail.component.scss']
})
export class GeneralEntryMasterDetailComponent implements OnInit {
  id: string = "";
  @Input() set Id(value: string) {
    this.id = value;
   // this.getDetail();
  }
  @Input() refreshData: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
