import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movieservice.service';
import { Movie } from '../models/movie';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrl: './movie.component.css'
})
export class MovieComponent implements OnInit {
  public movies: Movie[] = [];

  constructor(private router: Router, private movieService: MovieService) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.route = router.url
      }
    })
  }

  route = '';

  ngOnInit(): void {
    this.movieService
      .getMovies()
      .subscribe((result: Movie[]) => (this.movies = result));
  }
}
