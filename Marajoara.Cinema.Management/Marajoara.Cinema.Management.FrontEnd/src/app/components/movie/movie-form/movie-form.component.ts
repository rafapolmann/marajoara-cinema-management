import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormControlDirective, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { Movie } from 'src/app/models/Movie';

@Component({
  selector: 'app-movie-form',
  templateUrl: './movie-form.component.html',
  styleUrls: ['./movie-form.component.scss']
})
export class MovieFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<Movie>();
  @Output() onCancel = new EventEmitter();
  @Input() movieData!: Movie;
  posterImgSrc!: string;
  movieForm!: FormGroup;
  constructor() { }

  ngOnInit(): void {
    this.movieForm = new FormGroup({
      movieID: new FormControl(this.movieData ? this.movieData.movieID : ''),
      title: new FormControl(this.movieData ? this.movieData.title : '', [Validators.required]),
      description: new FormControl(this.movieData ? this.movieData.description : '', [Validators.required]),
      isOriginalAudio: new FormControl(this.movieData ? this.movieData.isOriginalAudio : true, [Validators.required]),
      is3D: new FormControl(this.movieData ? this.movieData.is3D : true, [Validators.required]),
      minutes: new FormControl(this.movieData ? this.movieData.minutes : 10, [Validators.required, Validators.max(600), Validators.min(10)]),
      posterFile: new FormControl(''),
    });
    console.log(this.movieForm);
    if (this.movieData && this.movieData.poster) {
      this.posterImgSrc = `data:image/png;base64,${this.movieData.poster}`;
    }
  }

  get title() {
    console.log("get");
    return this.movieForm.get('title')!;
  }

  get description() {
    return this.movieForm.get('description')!;
  }

  get minutes() {
    return this.movieForm.get('minutes')!;
  }
  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    this.movieForm.patchValue({ posterFile: file });



    const reader = new FileReader();
    reader.onload = e => this.posterImgSrc = String(reader.result);

    reader.readAsDataURL(file);

  }

  onCleanFile() {
    this.movieForm.patchValue({ posterFile: null });
    this.posterImgSrc = '';
  }

  onAudioSelect(value: any) {
    this.movieForm.patchValue({ isOriginalAudio: value });
  }

  cancel() {
    this.onCancel.emit();
  }
  submit(formDirective: FormGroupDirective): void {
    if (this.movieForm.invalid) {
      return;
    }
    this.onSubmit.emit(this.movieForm.value);
    console.log(this.movieForm.value);
    //Resets the form.
    this.movieForm.reset();
    formDirective.resetForm();

  }
}
