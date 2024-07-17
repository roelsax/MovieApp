import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Actor } from '../models/actor';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ActorService {
  apiUrl = "https://localhost:7258/actors/";
  constructor(private http: HttpClient) { }

  public getActors(): Observable<Actor[]>
  {
    return this.http.get<any>(this.apiUrl).pipe(
      map(response => {
        console.log(response);
        const data =  response.$values.map((actor: any) => ({
          directorId: actor.actorId,
          name: actor.name,
          dateOfBirth: actor.dateOfBirth,
          location: actor.location,
          nationality: actor.nationality,
          bio: actor.bio,
          picture: actor.picture,
          movies: actor.actorMovies.$values.map((actorMovieRef: any) => actorMovieRef.movie)
        }));
        console.log(data);
        return data;
      })
    )
  }

  public getActor(id: number): Observable<Actor> {
    return this.http.get<Actor>(`${this.apiUrl}${id}`)
  }
}
