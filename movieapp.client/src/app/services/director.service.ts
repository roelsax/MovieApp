import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Director } from '../models/director';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DirectorService {
  apiUrl = "https://localhost:7258/directors/";
  constructor(private http: HttpClient) { }

  public getDirectors(): Observable<Director[]> {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        return response.$values.map((director: any) => ({
          directorId: director.directorId,
          name: director.name,
          dateOfBirth: director.dateOfBirth,
          location: director.location,
          nationality: director.nationality,
          bio: director.bio,
          picture: director.picture,
          movies: director.movies.$values.map((movieRef: any) => movieRef.$ref)
        }));
      })
    )
  }

  public addDirector(director: any, onSuccess: () => void): void {
    this.http.post(`${this.apiUrl}create`, director)
      .subscribe((res) => {
        onSuccess();
      })
  }

  public deleteDirector(id: number, onSuccess: () => void): void {
    this.http.delete(`${this.apiUrl}delete/${id}`)
      .subscribe((res) => {
        onSuccess();
      })
  }

  public getDirector(id: number): Observable<Director> {
    return this.http.get<Director>(`${this.apiUrl}${id}`)
  }
}
