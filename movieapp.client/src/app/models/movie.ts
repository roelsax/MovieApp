import { Director } from "./director";
import { Actor } from "./actor";

export interface Movie {
  movieId: number;
  name: string;
  releaseDate: string;
  description: string;
  picture: string;
  director: Director;
  genres: any;
  genreDtos: any;
  actors: any;
}
