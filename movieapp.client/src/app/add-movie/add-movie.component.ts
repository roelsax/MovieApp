import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { DirectorService } from '../services/director.service';
import { Director } from '../models/director';
import { Actor } from '../models/actor';
import { ActorService } from '../services/actor.service';
import { CommonModule } from '@angular/common';
import { MovieService } from '../services/movieservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'add-movie',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
    MatIconModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './add-movie.component.html',
  styleUrl: './add-movie.component.css'
})
export class AddMovieComponent implements OnInit {
  public directors: Director[] = [];
  public actors: Actor[] = [];
  openDirectorDialog = false;
  openActorDialog = false;
  directorString = '';
  actorString = '';
  selectedGenre = '';
  directorSearchResults: Director[] = [];
  actorSearchResults: Actor[] = [];
  movieForm: FormGroup;
  genres: string[] = [];

  constructor(
    private directorService: DirectorService,
    private actorService: ActorService,
    private formBuilder: FormBuilder,
    private movieService: MovieService,
    private router: Router
  ) {
    this.movieForm = this.formBuilder.group({
      name: new FormControl(''),
      release_date: new FormControl(''),
      description: new FormControl(''),
      picture: new FormControl(null),
      directorId: new FormControl<number | null>(null),
      actors: this.formBuilder.array([]),
      genres: this.formBuilder.array([]),
    });
  }
  ngOnInit(): void {
    this.directorService
      .getDirectors()
      .subscribe((result: Director[]) => (this.directors = result));

    this.actorService
      .getActors()
      .subscribe((result: Actor[]) => (this.actors = result));

    this.movieService
      .getGenres()
      .subscribe((result: string[]) => (this.genres = result));
  }

  get actorsArray(): FormArray {
    return this.movieForm.get('actors') as FormArray;
  }

  get genresArray(): FormArray {
    return this.movieForm.get('genres') as FormArray;
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      const uploadedFile = event.target.files[0];
      const reader = new FileReader();

      reader.readAsDataURL(uploadedFile);
      reader.onload = () => {
        this.movieForm.patchValue({
          picture: {
            name: uploadedFile.name,
            image: reader.result
          }
        });
      };
    }
  }

  searchDirectors() {
    this.directorSearchResults = this.directors.filter((director) => this.filterName(this.directorString, director.name));
    this.openDirectorDialog = true;
  }

  searchActors() {
    this.actorSearchResults = this.actors.filter((actor) => this.filterName(this.actorString, actor.name));
    this.openActorDialog = true;
  }

  removeActor(index: number) {
    this.actorsArray.removeAt(index);
  }

  removeGenre(index: number) {
    this.genresArray.removeAt(index);
  }

  filterName(searchString: string, fullName: string) {
    const splitName = fullName.split(" ");

    return splitName[0].toLowerCase().startsWith(searchString) || splitName[1].toLowerCase().startsWith(searchString);
  }

  selectDirector(director: Director) {
    this.directorString = director.name;
    this.movieForm.controls['directorId'].setValue(director.directorId);
    this.openDirectorDialog = false;
  }

  selectActor(actor: Actor) {
    this.actorString = '';

    const actorControl = new FormGroup({
      id: new FormControl(actor.actorId),
      name: new FormControl(actor.name)
    });

    const actorsArray = this.movieForm.controls['actors'] as FormArray;
    actorsArray.push(actorControl);
    this.openActorDialog = false;
  }

  selectGenre(event: any) {
    const genre = this.genres[event.target.value];
    const actorControl = new FormGroup({
      id: new FormControl(event.target.value),
      name: new FormControl(genre)
    });

    const genresArray = this.movieForm.controls['genres'] as FormArray;
    genresArray.push(actorControl);
    this.selectedGenre = '';
  }

  submitForm() {
    const form = this.movieForm.value;

    let movie: {
      name: string;
      releaseDate: string;
      description: string | null;
      picture: File | null;
      directorId: number | null;
      genres: { id: number, name: string }[];
      actors: { id: number, name: string }[];
    } = {
      name: form.name,
      releaseDate: form.release_date,
      description: form.description,
      picture: this.movieForm.get('picture')?.value,
      directorId: form.directorId,
      genres: [],
      actors: []
    }

    form.actors.forEach((actor: any) => {
      movie.actors.push(actor);
    })

    form.genres.forEach((genre: any) => {
      movie.genres.push(genre);
    })
    this.movieService.addMovie(movie, () => {
      this.router.navigate(['/movies']);
    })
  }
}
