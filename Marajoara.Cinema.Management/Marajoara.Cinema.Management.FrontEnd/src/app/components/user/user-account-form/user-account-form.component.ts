import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserAccount } from 'src/app/models/UserAccount';
import { FormControl, FormGroup, FormGroupDirective, Validators, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'app-user-account-form',
  templateUrl: './user-account-form.component.html',
  styleUrls: ['./user-account-form.component.scss']
})
export class UserAccountFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<UserAccount>();
  @Output() onCancel = new EventEmitter();
  @Input() userAccountData!: UserAccount;

  userAccountForm!: FormGroup;
  matcher = new MyErrorStateMatcher();

  constructor() { }

  ngOnInit(): void {

    this.userAccountForm = new FormGroup({
      userAccountID: new FormControl(this.userAccountData ? this.userAccountData.userAccountID : ''),
      name: new FormControl(this.userAccountData ? this.userAccountData.name : '', [Validators.required]),
      mail: new FormControl(this.userAccountData ? this.userAccountData.mail : '', [Validators.required, Validators.email]),
      level: new FormControl(this.userAccountData ? this.userAccountData.level : 0, [Validators.required]),
      photoFile: new FormControl(''),
    });

  }

  get name() {
    return this.userAccountForm.get('name')! as FormControl;
  }

  get mail() {
    return this.userAccountForm.get('mail')! as FormControl;
  }

  get level() {
    return this.userAccountForm.get('level')! as FormControl;
  }

  submit(formDirective: FormGroupDirective): void {
    console.log(this.userAccountForm.value);
    if (this.userAccountForm.invalid) {
      return;
    }
    //this.onSubmit.emit(this.userAccountForm.value);

    //Resets the form.
    this.userAccountForm.reset();
    formDirective.resetForm();
  }

  cancel() {
    this.onCancel.emit();
  }
}

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
