import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../Page/header/header.component';
import { FooterComponent } from '../../Page/footer/footer.component';
import { NavBarComponent } from '../../Page/nav-bar/nav-bar.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent];

  const installedModule=[
    
  ]

@NgModule({
  declarations: [
    component
  ],
  imports: [
    CommonModule,installedModule,
    commonModules,
  ],
  exports: [component,
    commonModules,
    installedModule]
})
export class FeatureModule { }
