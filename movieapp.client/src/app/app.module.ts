import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavLayoutComponent } from './nav-layout-component/nav-layout-component.component';
import { RouterModule } from '@angular/router';
import { MovieComponent } from './movie/movie.component';

import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DirectorsComponent } from './directors/directors.component';
import { ActorsComponent } from './actors/actors.component';
import { DirectorDetailComponent } from './director-detail/director-detail.component';
import { ActorDetailComponent } from './actor-detail/actor-detail.component';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { FormsModule } from '@angular/forms';
import { DeleteConfirmationComponent } from './delete-confirmation/delete-confirmation.component';

@NgModule({
  declarations: [
    AppComponent,
    NavLayoutComponent,
    MovieComponent,
    DirectorsComponent,
    ActorsComponent,
    DirectorDetailComponent,
    ActorDetailComponent,
    MovieDetailComponent,
    DeleteConfirmationComponent
  ],
  imports: [
    RouterModule, FormsModule,
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
