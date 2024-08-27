import { Component, OnInit } from '@angular/core';
import { ActorService } from '../services/actor.service';
import { Actor } from '../models/actor';

@Component({
  selector: 'app-actors',
  templateUrl: './actors.component.html',
  styleUrl: './actors.component.css'
})
export class ActorsComponent implements OnInit {
  public actors: Actor[] = [];
  loading: boolean = true;
  constructor(private actorService: ActorService) { }

  ngOnInit(): void {
    this.actorService.getActors().subscribe((result: Actor[]) => {
      this.actors = result;
      this.loading = false;
    });
  }
}
