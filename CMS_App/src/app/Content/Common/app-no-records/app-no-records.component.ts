import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-no-records',
  templateUrl: './app-no-records.component.html',
  styleUrls: ['./app-no-records.component.scss']
})
export class AppNoRecordsComponent implements OnInit {
  @Input() data: any;
  @Output() onClick = new EventEmitter<void>();
  constructor() { }

  ngOnInit(): void {
  }

  onButtonClick() {
    this.onClick.next();
  }
}
