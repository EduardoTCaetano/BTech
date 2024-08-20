import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { AppComponent } from './app.component';
import { HomeComponent } from './features/pages/home/home.component';
import { CardsComponent } from './core/components/cards/cards.component';
import { IntroductionComponent } from './core/components/introduction/introduction.component';
import { AboutComponent } from './core/components/about/about.component';
import { CarouselComponent } from './core/components/carousel/carousel.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { AdvertisingComponent } from './core/components/advertising/advertising.component';

import { AngularFireModule } from '@angular/fire/compat';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { SacComponent } from './core/components/sac/sac.component';
import { CartComponent } from './core/components/cart/cart.component';
import { ProductPageComponent } from './core/components/product-page/product-page.component';
import { SearchComponent } from './core/components/search/search.component';
import { LoginComponent } from './core/components/login/login.component';
import { AuthService } from './services/auth/auth.service';
import { CartService } from './services/cart/cart.service';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    IntroductionComponent,
    CardsComponent,
    AdvertisingComponent,
    FooterComponent,
    CarouselComponent,
    AboutComponent,
    SacComponent,
    CartComponent,
    ProductPageComponent,
    SearchComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [AuthService, CartService],
  bootstrap: [AppComponent]
})
export class AppModule { }
