import { Director } from "./director";
import { Actor } from "./actor";
export interface Movie {
  movieId: number;
  name: string;
  releaseDate: string;
  description: string;
  picture: string;
  director: Director;
  genres: string[];
  actors: Actor[];
}
