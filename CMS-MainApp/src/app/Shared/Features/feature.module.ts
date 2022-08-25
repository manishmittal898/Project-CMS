import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { HeaderComponent } from './Pages/header/header.component';
import { FooterComponent } from './Pages/footer/footer.component';
import { NavBarComponent } from './Pages/nav-bar/nav-bar.component';
const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const installedModule = [
  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5
  }),

];

const sharedComponent = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent,
];

@NgModule({
  declarations: [
    sharedComponent,


  ],
  imports: [
    CommonModule,
    commonModules,
    installedModule
  ],
  exports: [
    commonModules,
    installedModule,
    sharedComponent
  ],
  entryComponents: []
})
export class FeatureModule { }
