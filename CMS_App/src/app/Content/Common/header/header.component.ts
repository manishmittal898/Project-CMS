import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../Shared/Helper/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() SetTheme = new EventEmitter<string>();
  constructor(readonly _authService: AuthService) { }

  ngOnInit(): void {
    setTimeout(() => {
      //   this.changeTheme();
    }, 10);

  }


  changeTheme(themeName: string = '') {
    this.SetTheme.emit(themeName);
  }
}


