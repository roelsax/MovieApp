import { Component, OnInit } from '@angular/core';
import { Actor } from '../models/actor';
import { ActivatedRoute } from '@angular/router';
import { ActorService } from '../services/actor.service';

@Component({
  selector: 'actor-detail',
  templateUrl: './actor-detail.component.html',
  styleUrl: './actor-detail.component.css'
})
export class ActorDetailComponent implements OnInit {
  public actor?: Actor;
  constructor(
    private route: ActivatedRoute,
    private actorService: ActorService
  ) { }
    ngOnInit(): void {
      const id = this.route.snapshot.paramMap.get('id');

      if (id != null) {
        this.actorService
          .getActor(parseInt(id))
          .subscribe(actor => this.actor = actor);
      }
    }
}
