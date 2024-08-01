import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavLayoutComponent } from './nav-layout-component/nav-layout-component.component';
import { MovieComponent } from './movie/movie.component';
import { AddMovieComponent } from './add-movie/add-movie.component';
import { DirectorsComponent } from './directors/directors.component';
import { ActorsComponent } from './actors/actors.component';
import { AddDirectorComponent } from './add-director/add-director.component';
import { AddActorComponent } from './add-actor/add-actor.component';
import { DirectorDetailComponent } from './director-detail/director-detail.component';
import { ActorDetailComponent } from './actor-detail/actor-detail.component';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { DeleteConfirmationComponent } from './delete-confirmation/delete-confirmation.component';

const routes: Routes = [
  {
    path: '',
    component: NavLayoutComponent,
    children: [
      { path: '', component: MovieComponent },
      { path: 'movies', component: MovieComponent },
      { path: 'add-movie', component: AddMovieComponent },
      { path: 'edit/movie/:id', component: AddMovieComponent },
      { path: 'movie/:id', component: MovieDetailComponent },
      { path: 'directors', component: DirectorsComponent },
      { path: 'actors', component: ActorsComponent },
      { path: 'add-actor', component: AddActorComponent },
      { path: 'edit/actor/:id', component: AddActorComponent },
      { path: 'actor/:id', component: ActorDetailComponent },
      { path: 'add-director', component: AddDirectorComponent },
      { path: 'edit/director/:id', component: AddDirectorComponent },
      { path: 'director/:id', component: DirectorDetailComponent },
      { path: 'delete/:model/:id', component: DeleteConfirmationComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
