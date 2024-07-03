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
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CommonModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
