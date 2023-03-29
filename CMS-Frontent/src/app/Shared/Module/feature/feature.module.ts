import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../Page/header/header.component';
import { FooterComponent } from '../../Page/footer/footer.component';
import { NavBarComponent } from '../../Page/nav-bar/nav-bar.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgSelectModule } from '@ng-select/ng-select';
import {CheckboxModule} from 'primeng-lts/checkbox';
import {SliderModule} from 'primeng/slider';
import { ToastrModule } from 'ngx-toastr';

const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent

];

const installedModule = [
  NgxPaginationModule,
  NgSelectModule,
  CheckboxModule,
  SliderModule,
  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5
  }),
]

@NgModule({
  declarations: [
    component
  ],
  imports: [
    CommonModule,
    installedModule,
    commonModules,
  ],
  exports: [
    component,
    commonModules,
    installedModule]
})
export class FeatureModule { }
