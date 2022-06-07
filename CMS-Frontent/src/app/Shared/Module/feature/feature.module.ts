import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from '../../Page/header/header.component';
import { FooterComponent } from '../../Page/footer/footer.component';
import { NavBarComponent } from '../../Page/nav-bar/nav-bar.component';

const component = [
  HeaderComponent,
  FooterComponent,
  NavBarComponent];


@NgModule({
  declarations: [
    component
  ],
  imports: [
    CommonModule
  ],
  exports: [component]
})
export class FeatureModule { }
