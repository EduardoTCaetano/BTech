import { AuthGuard } from './guard/authGuard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/pages/home/home.component';
import { SacComponent } from './core/components/sac/sac.component';
import { CartComponent } from './core/components/cart/cart.component';
import { SearchComponent } from './core/components/search/search.component';
import { LoginComponent } from './core/components/login/login.component';
import { ProductPageComponent } from './core/components/product-page/product-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent},
  { path: 'sac', component: SacComponent},
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'search', component: SearchComponent},
  { path: 'login', component: LoginComponent},
  { path: 'page/:id', component: ProductPageComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})

export class AppRoutingModule {}
