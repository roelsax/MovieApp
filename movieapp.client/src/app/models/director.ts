import { Movie } from "./movie";

export interface Director {
  directorId: number;
  name: string;
  dateOfBirth: string;
  location: string;
  nationality: string;
  bio: string;
  picture: string;
  movies: Movie[];
}
