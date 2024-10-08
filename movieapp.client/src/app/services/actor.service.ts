import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Actor } from '../models/actor';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  apiUrl = `${environment.applicationUrl}/actors/`;
  constructor(private http: HttpClient) { }

  public getActors(): Observable<Actor[]>
  {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        const data = response.$values.map((actor: any) => ({
          actorId: actor.actorId,
          name: actor.name,
          dateOfBirth: actor.dateOfBirth,
          location: actor.location,
          nationality: actor.nationality,
          bio: actor.bio,
          picture: actor.picture,
          movies: actor.actorMovies.$values.map((actorMovieRef: any) => actorMovieRef.movie)
        }));
        return data;
      })
    )
  }

  public addActor(actor: any, onSuccess: () => void, onError: (errors: any) => void): void {
    this.http.post(`${this.apiUrl}create`, actor)
      .subscribe((res) => {
        onSuccess();
      },
        (errorResponse) => {
          if (errorResponse.status === 400 && errorResponse.error) {
            onError(errorResponse.error);
          }
        })
  }

  public editActor(actor: any, id: number, onSuccess: () => void, onError: (errors: any) => void): void {
    this.http.put(`${this.apiUrl}${id}`, actor)
      .subscribe((res) => {
        onSuccess();
      },
        (errorResponse) => {
          if (errorResponse.status === 400 && errorResponse.error) {
            onError(errorResponse.error);
          }
        })
  }

  public deleteActor(id: number, onSuccess: () => void): void {
    this.http.delete(`${this.apiUrl}delete/${id}`)
      .subscribe((res) => {
        onSuccess();
      })
  }

  public getActor(id: number): Observable<Actor> {
    return this.http.get<Actor>(`${this.apiUrl}${id}`)
  }
}
