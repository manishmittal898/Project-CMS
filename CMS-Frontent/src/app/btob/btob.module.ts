import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BtoBRoutingModule } from './btob-routing.module';
import { BtoBComponent } from './btob.component';


@NgModule({
  declarations: [BtoBComponent],
  imports: [
    CommonModule,
    BtoBRoutingModule
  ]
})
export class BtoBModule { }
