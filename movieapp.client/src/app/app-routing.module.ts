import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavLayoutComponent } from './nav-layout-component/nav-layout-component.component';
import { MovieComponent } from './movie/movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { DirectorsComponent } from './directors/directors.component';
import { ActorsComponent } from './actors/actors.component';

const routes: Routes = [
  {
    path: '',
    component: NavLayoutComponent,
    children: [
      { path: '', component: MovieComponent },
      { path: 'movies', component: MovieComponent },
      { path: 'add-movie', component: AddMovieComponent },
      { path: 'directors', component: DirectorsComponent },
      { path: 'actors', component: ActorsComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
