import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { DashboardComponent } from './core/components/dashboard/dashboard.component';
import { AuthGuard } from './guard/auth.guard';
import { CategoryComponent } from './core/components/category/category.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full',  },
  { path: 'login', component: LoginComponent },
  { path: 'category', component: CategoryComponent, canActivate: [AuthGuard]  },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
