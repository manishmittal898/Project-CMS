import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../Shared/Helper/auth.service';
declare var $: any;

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
      this.SidebarBtn();
    }, 10);

  }


  changeTheme(themeName: string = '') {
    this.SetTheme.emit(themeName);
  }
  
  SidebarBtn()
  {
    $(".sidebar-menu-btn").click(function () {
      $("body").toggleClass("sidebar-open");
    });

    $("ul.sidebar-submenu a, .sidebar-item-button.arrow-none").click(function () {
      $("body").removeClass("sidebar-open");
    });
  }
}


