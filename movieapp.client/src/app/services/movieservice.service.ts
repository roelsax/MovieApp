import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../models/movie';
import { Director } from '../models/director';
import { Actor } from '../models/actor';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class MovieService {
  apiUrl = "https://localhost:7258/movies/";
  constructor(private http: HttpClient) { }

  
  public getMovies(): Observable<Movie[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        return response.$values.map((movie: any) => ({
          movieId: movie.movieId,
          name: movie.name,
          releaseDate: movie.releaseDate,
          description: movie.description,
          picture: movie.picture,
          director: {
            directorId: movie.director.directorId,
            name: movie.director.name,
            dateOfBirth: movie.director.dateOfBirth,
            location: movie.director.location,
            nationality: movie.director.nationality,
            bio: movie.director.bio,
            picture: movie.director.picture
          },
          genres: movie.genres.$values,
          actors: movie.actors.$values.map((actorRef: any) => actorRef.$ref)
        }));
      })
    );
  }

  public getMovie(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}${id}`)
  }

  public addMovie(movie: any, onSuccess: () => void): void {
    this.http.post(`${this.apiUrl}create`, movie)
      .subscribe((res) => {
        onSuccess();
      })
  }


  public getGenres(): Observable<string[]>
  {
    return this.http.get<any>(`${this.apiUrl}genres`);
  }
}
