import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MovieService } from '../services/movieservice.service';
import { DirectorService } from '../services/director.service';
import { ActorService } from '../services/actor.service';

@Component({
  selector: 'app-delete-confirmation',
  templateUrl: './delete-confirmation.component.html',
  styleUrl: './delete-confirmation.component.css'
})
export class DeleteConfirmationComponent implements OnInit {
  public model: string = '';
  public id: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private movieService: MovieService,
    private directorService: DirectorService,
    private actorService: ActorService,
    private router: Router
  ) { }
  ngOnInit(): void {
    this.model = this.route.snapshot.paramMap.get('model') ?? '';

    this.id = this.route.snapshot.paramMap.get('id');
  }

  deleteModel(model: string, id: any) {
    switch (model) {
      case 'movie':
        this.movieService.deleteMovie(id, () => {
          this.router.navigate(['/movies'])
        })
        break;
      case 'director':
        this.directorService.deleteDirector(id, () => {
          this.router.navigate(['/directors'])
        })
        break;
      case 'actor':
        this.actorService.deleteActor(id, () => {
          this.router.navigate(['/actors'])
        })
        break;
    }
  }
}
