import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboneBorcComponent } from './abone-borc/abone-borc.component';
import { AboneDetailComponent } from './abone-detail/abone-detail.component';
import { AddAboneComponent } from './add-abone/add-abone.component';
import { CalculationComponent } from './calculation/calculation.component';
import { HomeComponent } from './home/home.component';
import { TutarCalculationComponent } from './tutar-calculation/tutar-calculation.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'home',redirectTo:''},
  {path:'calculation/:id',component:CalculationComponent},
  {path:'abone-detail/:id',component:AboneDetailComponent},
  {path:'add-abone',component:AddAboneComponent},
  {path:'abone-borc/:id',component:AboneBorcComponent},
  {path:'tutar-calculation/:id',component:TutarCalculationComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
