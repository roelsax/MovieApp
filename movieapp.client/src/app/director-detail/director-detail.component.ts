import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Director } from '../models/director';
import { DirectorService } from '../services/director.service';
import { Movie } from '../models/movie';

@Component({
  selector: 'director-detail',
  templateUrl: './director-detail.component.html',
  styleUrl: './director-detail.component.css'
})
export class DirectorDetailComponent implements OnInit {
  public director?: Director;
  constructor(
    private route: ActivatedRoute,
    private directorService: DirectorService
  ) { }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id != null)
    {
      this.directorService
        .getDirector(parseInt(id))
        .subscribe(director => this.director = director);
    }
  }
}
