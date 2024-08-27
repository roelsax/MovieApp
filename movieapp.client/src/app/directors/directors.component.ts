import { Component, OnInit } from '@angular/core';
import { DirectorService } from '../services/director.service';
import { Director } from '../models/director';

@Component({
  selector: 'app-directors',
  templateUrl: './directors.component.html',
  styleUrl: './directors.component.css'
})
export class DirectorsComponent implements OnInit {
  public directors: Director[] = [];
  loading: boolean = true;
  constructor(private directorService: DirectorService) { }

    ngOnInit(): void {
      this.directorService
        .getDirectors()
        .subscribe((result: Director[]) => {
          this.directors = result;
          this.loading = false;
        });
    }
}
