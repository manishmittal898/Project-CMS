import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CMSPageAddUpdateComponent } from "./cmspage-master/cmspage-add-update/cmspage-add-update.component";
import { CMSPageMasterComponent } from "./cmspage-master/cmspage-master.component";
import { LookupTypeAddEditComponent } from "./lookup-type/lookup-type-add-edit/lookup-type-add-edit.component";
import { LookupTypeComponent } from "./lookup-type/lookup-type.component";
import { LookupsAddEditComponent } from "./lookup-type/lookups/lookups-add-edit/lookups-add-edit.component";
import { LookupsComponent } from "./lookup-type/lookups/lookups.component";
import { SubLookupComponent } from "./lookup-type/lookups/sub-lookup/sub-lookup.component";
import { GeneralEntryMasterComponent } from './general-entry-master/general-entry-master.component';
import { GeneralEntryMasterAddEditComponent } from './general-entry-master/general-entry-master-add-edit/general-entry-master-add-edit.component';
import { GeneralEntryMasterDetailComponent } from './general-entry-master/general-entry-master-detail/general-entry-master-detail.component';
import { GeneralEntryCategoryMasterAddEditComponent } from "./general-entry-master/general-entry-category-master/general-entry-category-master-add-edit/general-entry-category-master-add-edit.component";
import { GeneralEntryCategoryMasterDetailComponent } from "./general-entry-master/general-entry-category-master/general-entry-category-master-detail/general-entry-category-master-detail.component";
import { GeneralEntryCategoryMasterComponent } from "./general-entry-master/general-entry-category-master/general-entry-category-master.component";
import { GeneralEntrySubCategoryMasterAddEditComponent } from "./general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master-add-edit/general-entry-sub-category-master-add-edit.component";
import { GeneralEntrySubCategoryMasterDetailComponent } from "./general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master-detail/general-entry-sub-category-master-detail.component";
import { GeneralEntrySubCategoryMasterComponent } from "./general-entry-master/general-entry-category-master/general-entry-sub-category-master/general-entry-sub-category-master.component";

const routes: Routes = [
  { path: '', component: LookupTypeComponent },

  { path: 'cms-page', component: CMSPageMasterComponent },
  { path: 'cms-page/:name', component: CMSPageAddUpdateComponent },


  { path: 'lookup-type', component: LookupTypeComponent },
  { path: 'lookup-type/add', component: LookupTypeAddEditComponent },
  { path: 'lookup-type/edit/:id', component: LookupTypeAddEditComponent },

  { path: ':name/:typeId', component: LookupsComponent },
  { path: 'lookup/add', component: LookupsAddEditComponent },
  { path: 'lookup/:name/update/:id', component: LookupsAddEditComponent },


  { path: ':name/:subname/:lookupId', component: SubLookupComponent },
  { path: 'lookup/add', component: LookupsAddEditComponent },
  { path: 'lookup/:name/update/:id', component: LookupsAddEditComponent },


  { path: 'general-entry', component: GeneralEntryMasterComponent },
  { path: 'general-entry/add', component: GeneralEntryMasterAddEditComponent },
  { path: 'general-entry/edit', component: GeneralEntryMasterAddEditComponent },
  { path: 'general-entry/detail', component: GeneralEntryMasterDetailComponent },

  { path: 'general-entry-category', component: GeneralEntryCategoryMasterComponent },
  { path: 'general-entry-category/add', component: GeneralEntryCategoryMasterAddEditComponent },
  { path: 'general-entry-category/edit', component: GeneralEntryCategoryMasterAddEditComponent },
  { path: 'general-entry-category/detail', component: GeneralEntryCategoryMasterDetailComponent },


  { path: 'general-entry-sub-category', component: GeneralEntrySubCategoryMasterComponent },
  { path: 'general-entry-sub-category/add', component: GeneralEntrySubCategoryMasterAddEditComponent },
  { path: 'general-entry-sub-category/edit', component: GeneralEntrySubCategoryMasterAddEditComponent },
  { path: 'general-entry-sub-category/detail', component: GeneralEntrySubCategoryMasterDetailComponent },

  // {path:'SubLookupType',component: SubLookupTypeComponent},
  // {path:'SubLookupTypeAddEdit',component: SubLookupTypeAddEditComponent},
  // {path:'SubLookup',component: SubLookupComponent},
  // {path:'SubLookupAddEdit',component: SubLookupAddEditComponent}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
