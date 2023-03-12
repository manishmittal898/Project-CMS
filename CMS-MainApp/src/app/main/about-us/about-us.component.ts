import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'ngx-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {
  isTrue: boolean=true;
  constructor() { }

  ngOnInit(): void {
    this.isTrue=true
  }

}
