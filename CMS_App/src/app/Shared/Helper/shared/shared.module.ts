
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
import { FileSelectorComponent } from './file-selector/file-selector.component';
import { FooterComponent } from 'src/app/Content/Common/footer/footer.component';
import { HeaderComponent } from 'src/app/Content/Common/header/header.component';
import { LoaderComponent } from 'src/app/Content/Common/loader/loader.component';
import { NavigationComponent } from 'src/app/Content/Common/navigation/navigation.component';
import { PageNotFoundComponent } from 'src/app/Content/Common/page-not-found/page-not-found.component';
import { HtmlComponent } from 'src/app/Content/html/html.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
const commonModules = [
  HttpClientModule,
  ReactiveFormsModule,
  FormsModule,
  RouterModule
]

const installedModule = [

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
];

const sharedComponent = [FileSelectorComponent,
  HeaderComponent,
  FooterComponent,
  PageNotFoundComponent,
  NavigationComponent,
  LoaderComponent,

];

@NgModule({
  declarations: [
    sharedComponent,
    HtmlComponent,
  ],
  imports: [
    CommonModule,

    commonModules,
    installedModule,
  ],
  exports: [
    commonModules,
    installedModule,
    sharedComponent,
  ],
  entryComponents:[NavigationComponent]
})
export class SharedModule { }
