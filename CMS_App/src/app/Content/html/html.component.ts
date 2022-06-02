import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-html',
  templateUrl: './html.component.html',
  styleUrls: ['./html.component.scss']
})
export class HtmlComponent implements OnInit {
  items  = [
    {Value: 1, Text: 'Python'},
    {Value: 2, Text: 'Node Js'},
    {Value: 3, Text: 'Java'},
    {Value: 4, Text: 'PHP', disabled: true},
    {Value: 5, Text: 'Django'},
    {Value: 6, Text: 'Angular'},
    {Value: 7, Text: 'Vue'},
    {Value: 8, Text: 'ReactJs'},
  ];
  selected = [
    {Value: 2, Text: 'Node Js'},
    {Value: 8, Text: 'ReactJs'}
  ];
  constructor() { }

  ngOnInit(): void {
  }

}
