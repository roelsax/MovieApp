import { Component, OnInit } from '@angular/core';
import { DirectorService } from '../services/director.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { ActorService } from '../services/actor.service';
import { Actor } from '../models/actor'

@Component({
  selector: 'add-actor',
  templateUrl: './add-actor.component.html',
  styleUrl: './add-actor.component.css',
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
})
export class AddActorComponent implements OnInit {
  actorForm: FormGroup;
  selectedFile: File | null = null;
  EditActor: Actor | null = null;
  editMode: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private actorService: ActorService,
    private router: Router,
    private formBuilder: FormBuilder

  ) {
    this.actorForm = this.formBuilder.group({
      name: new FormControl(''),
      date_of_birth: new FormControl(''),
      bio: new FormControl(''),
      location: new FormControl(''),
      nationality: new FormControl(''),
      picture: new FormControl(null),
    })
  }
  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    
    if (id) {
      this.editMode = true;
      this.actorService.getActor(parseInt(id))
        .subscribe((res) => {
          this.EditActor = res;
          this.fillInActorToEdit();
        })
    }
  }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.actorForm.patchValue({
        picture: this.selectedFile
      });
    }
  }

  fillInActorToEdit() {
    this.actorForm.patchValue({
      name: this.EditActor?.name,
      date_of_birth: this.EditActor?.dateOfBirth,
      bio: this.EditActor?.bio,
      location: this.EditActor?.location,
      nationality: this.EditActor?.nationality,

    })
  }

  submitForm() {
    const form = this.actorForm.value;
    let formData = new FormData();
    formData.append('name', form.name);
    formData.append('dateOfBirth', new Date(form.date_of_birth).toISOString());
    formData.append('bio', form.bio);
    formData.append('location', form.location);
    formData.append('nationality', form.nationality);

    if (this.selectedFile) {
      formData.append('picture', this.selectedFile);
    }

    this.actorService.addActor(formData, () => {
      this.router.navigate(['/actors']);
    })
  }
}
