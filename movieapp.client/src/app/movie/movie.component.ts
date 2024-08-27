import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movieservice.service';
import { Movie } from '../models/movie';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrl: './movie.component.css',
})
export class MovieComponent implements OnInit {
  public movies: Movie[] = [];
  genres: string[] = [];
  searchString: string = '';
  queryGenre: string = '';
  selectedGenre: string = '';
  loading: boolean = true;

  constructor(private router: Router, private movieService: MovieService, private activatedRoute: ActivatedRoute) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.route = router.url
      }
    })
  }

  route = '';

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.searchString = params['search'] || ''; 
      this.selectedGenre = params['genre'] || '';
      this.queryGenre = params['genre'] || '';
    });
    
    this.fetchMovies(this.searchString, this.selectedGenre);

    this.movieService
      .getGenres()
      .subscribe((result: string[]) => (this.genres = result));
  }

  fetchMovies(search: string, genre: string) {
    this.movieService
      .getMovies(search, genre)
      .subscribe((result: Movie[]) => {
        this.movies = result;
        this.loading = false;
      });
  }

  filteredMovies() {
    this.fetchMovies(this.searchString, this.selectedGenre);
    this.queryGenre = this.selectedGenre;
  }

}
