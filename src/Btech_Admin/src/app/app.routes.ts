import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { DashboardComponent } from './core/components/dashboard/dashboard.component';
import { CategoryComponent } from './core/components/category/category.component';
import { AuthGuard } from './guard/auth.guard';
import { AccessDeniedComponent } from './core/components/access-denied/access-denied.component';
import { OrderComponent } from './core/components/order/order.component';
import { HomeComponent } from './core/components/home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'category', component: CategoryComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard], data: { role: 'Admin' } },
  { path: 'order', component: OrderComponent, canActivate: [AuthGuard], data: { role: 'Order' } },
  { path: 'access-denied', component: AccessDeniedComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
