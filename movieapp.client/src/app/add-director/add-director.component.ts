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
import { Director } from '../models/director';

@Component({
  selector: 'add-director',
  templateUrl: './add-director.component.html',
  styleUrl: './add-director.component.css',
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
export class AddDirectorComponent implements OnInit {
  directorForm: FormGroup;
  selectedFile: File | null = null;
  editDirector: Director | null = null;
  editMode: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private directorService: DirectorService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.directorForm = this.formBuilder.group({
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
        this.directorService.getDirector(parseInt(id))
          .subscribe((res) => {
            this.editDirector = res;
            this.fillInActorToEdit();
          })
      }
    }

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.directorForm.patchValue({
        picture: this.selectedFile
      });
    }
  }

  async fillInActorToEdit() {

    const file = this.editDirector?.picture ? await this.base64toFile(this.editDirector.picture) : null;
    this.selectedFile = this.editDirector?.picture.ImagePath != "" ? file : null;

    this.directorForm.patchValue({
      name: this.editDirector?.name,
      date_of_birth: this.editDirector?.dateOfBirth,
      bio: this.editDirector?.bio,
      location: this.editDirector?.location,
      nationality: this.editDirector?.nationality,
      picture: this.editDirector?.picture.ImagePath != "" ? file : null
    })
  }

  async base64toFile(picture: any): Promise<File> {
    let splitName = picture.imagePath?.split(".");
    const res: Response = await fetch("data:image/png;base64," + picture.base64);
    const blob: Blob = await res.blob();
    return new File([blob], picture.imagePath, { type: `image/${splitName[1]}` });
  }

  submitForm() {
    const form = this.directorForm.value;
    let formData = new FormData();
    formData.append('name', form.name);
    formData.append('dateOfBirth', new Date(form.date_of_birth).toISOString());
    formData.append('bio', form.bio);
    formData.append('location', form.location);
    formData.append('nationality', form.nationality);

    if (this.selectedFile) {
      formData.append('picture', this.selectedFile);
    }

    if (localStorage.getItem('movieData')) {
      this.directorService.addDirector(formData, () => {
        this.router.navigate(['/add-movie']);
      })
      return;
    }

    if (this.editMode && this.editDirector != null)
    {
      this.directorService.editDirector(formData, this.editDirector.directorId, () => {
        this.router.navigate(['/directors']);
      })
    } else {
    this.directorService.addDirector(formData, () => {
      this.router.navigate(['/directors']);
    })
    }
  }
}
