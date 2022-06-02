import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }

  constructor(private readonly _authService: AuthService) {
    
  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();

    this._authService.IsAuthentication.subscribe(x => {

    });
  }
}
