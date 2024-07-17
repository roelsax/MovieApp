import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavLayoutComponent } from './nav-layout-component/nav-layout-component.component';
import { RouterModule } from '@angular/router';
import { MovieComponent } from './movie/movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DirectorsComponent } from './directors/directors.component';
import { ActorsComponent } from './actors/actors.component';

@NgModule({
  declarations: [
    AppComponent,
    NavLayoutComponent,
    MovieComponent,
    DirectorsComponent,
    ActorsComponent,
    
  ],
  imports: [
    RouterModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
