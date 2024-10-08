import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Movie } from '../models/movie';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  apiUrl = `${environment.applicationUrl}/movies/`;
  constructor(private http: HttpClient) { }

  
  public getMovies(searchString: string, genre: string): Observable<Movie[]> {
    let params = new HttpParams();
    if (searchString !== '') { params = params.append('search', searchString); }
    if (genre !== "") { params = params.append('genre', genre); }
    
    return this.http.get<any>(this.apiUrl, { params: params }).pipe(
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

  public addMovie(movie: any, onSuccess: () => void, onError: (errors: any) => void): void {
    this.http.post(`${this.apiUrl}create`, movie)
      .subscribe((res) => {
        onSuccess();
      },
        (errorResponse) => {
          if (errorResponse.status === 400 && errorResponse.error) {
            onError(errorResponse.error);
          }
        }
      )
  }

  public editMovie(movie: any, id: number, onSuccess: () => void, onError: (errors: any) => void): void {
    this.http.put(`${this.apiUrl}${id}`, movie)
      .subscribe((res) => {
        onSuccess();
      },
        (errorResponse) => {
          if (errorResponse.status === 400 && errorResponse.error) {
            onError(errorResponse.error);
          }
        })
  }

  public deleteMovie(id: number, onSuccess: () => void): void {
    this.http.delete(`${this.apiUrl}delete/${id}`)
      .subscribe((res) => {
        onSuccess();
      })
  }

  public getGenres(): Observable<string[]>
  {
    return this.http.get<any>(`${this.apiUrl}genres`);
  }
}
