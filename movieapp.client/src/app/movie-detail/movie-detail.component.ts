import { Component, OnInit } from '@angular/core';
import { Movie } from '../models/movie';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { MovieService } from '../services/movieservice.service';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';

@Component({
  selector: 'movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrl: './movie-detail.component.css',
  
})
export class MovieDetailComponent implements OnInit {
  public movie?: Movie;
  loading: boolean = true;
  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService
  ) { }
    ngOnInit(): void {
      const id = this.route.snapshot.paramMap.get('id');

      if (id != null) {
        this.movieService
          .getMovie(parseInt(id))
          .subscribe(movie => this.movie = movie);
      } else {
        this.loading = false;
      }
    }
}
