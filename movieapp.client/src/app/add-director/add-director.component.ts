import { Component } from '@angular/core';
import { DirectorService } from '../services/director.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';

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
export class AddDirectorComponent {
  directorForm: FormGroup;
  selectedFile: File | null = null;

  constructor(
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

  onFileChange(event: any) {
    if (event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
      this.directorForm.patchValue({
        picture: this.selectedFile
      });
    }
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

    this.directorService.addDirector(formData, () => {
      this.router.navigate(['/directors']);
    })
  }
}
