import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LookupTypeAddEditComponent } from './lookup-type/lookup-type-add-edit/lookup-type-add-edit.component';
import { LookupTypeComponent } from './lookup-type/lookup-type.component';
import { LookupsAddEditComponent } from './lookup-type/lookups/lookups-add-edit/lookups-add-edit.component';
import { LookupsComponent } from './lookup-type/lookups/lookups.component';
import { SubLookupTypeAddEditComponent } from './sub-lookup-type/sub-lookup-type-add-edit/sub-lookup-type-add-edit.component';
import { SubLookupTypeComponent } from './sub-lookup-type/sub-lookup-type.component';
import { SubLookupAddEditComponent } from './sub-lookup-type/sub-lookup/sub-lookup-add-edit/sub-lookup-add-edit.component';
import { SubLookupComponent } from './sub-lookup-type/sub-lookup/sub-lookup.component';

const routes: Routes = [
  { path: '', component: LookupTypeComponent },

  { path: 'lookup-type', component: LookupTypeComponent },
  { path: 'lookup-type/add', component: LookupTypeAddEditComponent },
  { path: 'lookup-type/edit/:id', component: LookupTypeAddEditComponent },

  // {path:'Lookups',component: LookupsComponent},
  // {path:'LookupsAddEdit',component: LookupsAddEditComponent},
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
