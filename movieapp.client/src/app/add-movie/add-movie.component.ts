import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { DirectorService } from '../services/director.service';
import { Director } from '../models/director';
import { Actor } from '../models/actor';
import { ActorService } from '../services/actor.service';

@Component({
  selector: 'add-movie',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    MatNativeDateModule,
    MatIconModule
  ],
  templateUrl: './add-movie.component.html',
  styleUrl: './add-movie.component.css'
})
export class AddMovieComponent implements OnInit{
  public directors: Director[] = [];
  public actors: Actor[] = [];
  constructor(
    private directorService: DirectorService,
    private actorService: ActorService
  ) { }
  ngOnInit(): void {
    this.directorService
      .getDirectors()
      .subscribe((result: Director[]) => (this.directors = result));
    }

  movieForm = new FormGroup({
    name: new FormControl(''),
    release_date: new FormControl(''),
    description: new FormControl(''),
    picture: new FormControl(null),
    director: new FormControl(''),
    actors: new FormControl([]),
    genres: new FormControl([])
  });

  submitForm() {
    console.log(this.movieForm.value);
  }
}
