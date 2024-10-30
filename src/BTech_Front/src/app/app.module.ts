import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AngularFireModule } from '@angular/fire/compat';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { HomeComponent } from './features/pages/home/home.component';
import { CardsComponent } from './core/components/cards/cards.component';
import { IntroductionComponent } from './core/components/introduction/introduction.component';
import { AboutComponent } from './core/components/about/about.component';
import { CarouselComponent } from './core/components/carousel/carousel.component';
import { FooterComponent } from './core/components/footer/footer.component';
import { AdvertisingComponent } from './core/components/advertising/advertising.component';
import { SacComponent } from './core/components/sac/sac.component';
import { CartComponent } from './core/components/cart/cart.component';
import { ProductPageComponent } from './core/components/product-page/product-page.component';
import { SearchComponent } from './core/components/search/search.component';
import { LoginComponent } from './core/components/login/login.component';

import { AuthService } from './services/auth/auth.service';
import { CartService } from './services/cart/cart.service';
import { TechweekComponent } from './core/components/techweek/techweek.component';
import { AnnouncementComponent } from './core/components/announcement/announcement.component';
import { PaymentComponent } from './core/components/payment/payment.component';
import { NgxPayPalModule } from 'ngx-paypal';
import { SuccessComponent } from './core/components/success/success.component';
import { EmAltaComponent } from './core/components/em-alta/em-alta.component';

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
    TechweekComponent,
    AnnouncementComponent,
    PaymentComponent,
    SuccessComponent,
    EmAltaComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ReactiveFormsModule,
    NgxPayPalModule
  ],
  providers: [AuthService, CartService],
  bootstrap: [AppComponent],
})
export class AppModule {}
