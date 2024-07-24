import { Movie } from "./movie";

export interface Actor {
  actorId: number;
  name: string;
  dateOfBirth: string;
  location: string;
  nationality: string;
  bio: string;
  picture: string;
  actorMovies: any;
}
