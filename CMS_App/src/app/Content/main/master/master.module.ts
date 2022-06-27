import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MasterRoutingModule } from './master-routing.module';
import { LookupTypeComponent } from './lookup-type/lookup-type.component';
import { LookupTypeAddEditComponent } from './lookup-type/lookup-type-add-edit/lookup-type-add-edit.component';
import { LookupsComponent } from './lookup-type/lookups/lookups.component';
import { LookupsAddEditComponent } from './lookup-type/lookups/lookups-add-edit/lookups-add-edit.component';
import { SubLookupTypeComponent } from './sub-lookup-type/sub-lookup-type.component';
import { SubLookupTypeAddEditComponent } from './sub-lookup-type/sub-lookup-type-add-edit/sub-lookup-type-add-edit.component';
import { SubLookupComponent } from './sub-lookup-type/sub-lookup/sub-lookup.component';
import { SubLookupAddEditComponent } from './sub-lookup-type/sub-lookup/sub-lookup-add-edit/sub-lookup-add-edit.component';
import { SharedModule } from '../../../Shared/Helper/shared/shared.module';


@NgModule({
  declarations: [
    LookupTypeComponent,
    LookupTypeAddEditComponent,
    LookupsComponent,
    LookupsAddEditComponent,
    SubLookupTypeComponent,
    SubLookupTypeAddEditComponent,
    SubLookupComponent,
    SubLookupAddEditComponent
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,
    SharedModule
  ]
})
export class MasterModule { }
