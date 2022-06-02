
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToastrModule } from 'ngx-toastr';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MatStepperModule } from '@angular/material/stepper';
const CommonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,

]

const InstalledModule = [

  BsDatepickerModule.forRoot(),
  MatDialogModule,
  MatSnackBarModule,
  MatTableModule,
  MatSortModule,

  MatPaginatorModule,
  MatSortModule,
  NgSelectModule,
  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5
  }),
  MatStepperModule
]

//const SharedComponent = []



@NgModule({
  declarations: [
  //  SharedComponent,
  ],
  imports: [
    CommonModule,
    CommonModules,
    InstalledModule
  ],
  exports: [
    CommonModules,
    InstalledModule,
    // SharedComponent
  ]
})
export class SharedModule { }
