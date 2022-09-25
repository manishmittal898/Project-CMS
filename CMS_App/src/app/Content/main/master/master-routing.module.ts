import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { CMSPageAddUpdateComponent } from "./cmspage-master/cmspage-add-update/cmspage-add-update.component";
import { CMSPageMasterComponent } from "./cmspage-master/cmspage-master.component";
import { LookupTypeAddEditComponent } from "./lookup-type/lookup-type-add-edit/lookup-type-add-edit.component";
import { LookupTypeComponent } from "./lookup-type/lookup-type.component";
import { LookupsAddEditComponent } from "./lookup-type/lookups/lookups-add-edit/lookups-add-edit.component";
import { LookupsComponent } from "./lookup-type/lookups/lookups.component";
import { SubLookupComponent } from "./lookup-type/lookups/sub-lookup/sub-lookup.component";

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
