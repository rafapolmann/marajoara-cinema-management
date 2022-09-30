import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, FormGroupDirective, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Session } from 'src/app/models/Session';

@Component({
  selector: 'app-session-form',
  templateUrl: './session-form.component.html',
  styleUrls: ['./session-form.component.scss'],
})
export class SessionFormComponent implements OnInit {
  @Output() onSubmit= new EventEmitter<Session>();
  @Output() onCancel = new EventEmitter();
  @Input() sessionData!: Session;
  sessionForm!: FormGroup;
  constructor() {}

  ngOnInit(): void {}

  cancel() {
    this.onCancel.emit();
  }

  submit(formDirective: FormGroupDirective): void {
    console.log(this.sessionForm);
    if (this.sessionForm.invalid) {
      return;
    }
    this.onSubmit.emit(this.sessionForm.value);
    //Resets the form.
    this.sessionForm.reset();
    formDirective.resetForm();
  }
}
