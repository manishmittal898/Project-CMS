import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MasterRoutingModule } from './master-routing.module';
import { LookupTypeComponent } from './lookup-type/lookup-type.component';
import { LookupTypeAddEditComponent } from './lookup-type/lookup-type-add-edit/lookup-type-add-edit.component';
import { LookupsComponent } from './lookup-type/lookups/lookups.component';
import { LookupsAddEditComponent } from './lookup-type/lookups/lookups-add-edit/lookups-add-edit.component';
import { SharedModule } from '../../../Shared/Helper/shared/shared.module';
import { SubLookupAddEditComponent } from './lookup-type/lookups/sub-lookup/sub-lookup-add-edit/sub-lookup-add-edit.component';
import { SubLookupComponent } from './lookup-type/lookups/sub-lookup/sub-lookup.component';
import { CMSPageMasterComponent } from './cmspage-master/cmspage-master.component';
import { CMSPageAddUpdateComponent } from './cmspage-master/cmspage-add-update/cmspage-add-update.component';
import { GeneralEntryMasterComponent } from './general-entry-master/general-entry-master.component';
import { GeneralEntryMasterAddEditComponent } from './general-entry-master/general-entry-master-add-edit/general-entry-master-add-edit.component';
import { GeneralEntryMasterDetailComponent } from './general-entry-master/general-entry-master-detail/general-entry-master-detail.component';
import { GeneralEntryCategoryMasterComponent } from './general-entry-master/general-entry-category-master/general-entry-category-master.component';
import { GeneralEntryCategoryMasterAddEditComponent } from './general-entry-master/general-entry-category-master/general-entry-category-master-add-edit/general-entry-category-master-add-edit.component';
import { GeneralEntryCategoryMasterDetailComponent } from './general-entry-master/general-entry-category-master/general-entry-category-master-detail/general-entry-category-master-detail.component';
import { GeneralEntrySubCategoryMasterComponent } from './general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master.component';
import { GeneralEntrySubCategoryMasterDetailComponent } from './general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master-detail/general-entry-sub-category-master-detail.component';
import { GeneralEntrySubCategoryMasterAddEditComponent } from './general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master-add-edit/general-entry-sub-category-master-add-edit.component';


@NgModule({
  declarations: [
    LookupTypeComponent,
    LookupTypeAddEditComponent,
    LookupsComponent,
    LookupsAddEditComponent,
    SubLookupComponent,
    SubLookupAddEditComponent,
    CMSPageMasterComponent,
    CMSPageAddUpdateComponent,
    GeneralEntryMasterComponent,
    GeneralEntryMasterAddEditComponent,
    GeneralEntryMasterDetailComponent,
    GeneralEntryCategoryMasterComponent,
    GeneralEntryCategoryMasterAddEditComponent,
    GeneralEntryCategoryMasterDetailComponent,
    GeneralEntrySubCategoryMasterComponent,
    GeneralEntrySubCategoryMasterDetailComponent,
    GeneralEntrySubCategoryMasterAddEditComponent
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,
    SharedModule
  ]
})
export class MasterModule { }
