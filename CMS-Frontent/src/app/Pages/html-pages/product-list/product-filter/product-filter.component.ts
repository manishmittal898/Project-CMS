import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DropDownModel } from 'src/app/Shared/Helper/Common';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {
  @Input() filterModel: any;
  @Output() onFilterChange = new EventEmitter<any>();
  ddlModel :DropDownModel;
  constructor() { }

  ngOnInit(): void {
    
  }

}
