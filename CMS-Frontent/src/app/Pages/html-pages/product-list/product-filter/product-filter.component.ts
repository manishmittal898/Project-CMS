import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {
  @Input() filterModel: any;
  @Output() onFilterChange = new EventEmitter<any>();
  constructor() { }

  ngOnInit(): void {
  }

}
