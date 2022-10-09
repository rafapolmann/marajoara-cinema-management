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
  isEditForm: boolean = true;
  accountLevels: string[] = ['Cliente', 'Atendente', 'Gerente'];

  constructor() { }

  ngOnInit(): void {
    this.userAccountForm = new FormGroup({
      userAccountID: new FormControl(this.userAccountData ? this.userAccountData.userAccountID : ''),
      name: new FormControl(this.userAccountData ? this.userAccountData.name : '', [Validators.required]),
      mail: new FormControl(this.userAccountData ? this.userAccountData.mail : '', [Validators.required, Validators.email]),
      level: new FormControl(this.userAccountData ? this.getAccountLevel(this.userAccountData.level) : '', [Validators.required]),
      photoFile: new FormControl(''),
    });

    this.isEditForm = this.userAccountData ? true : false;
  }

  getAccountLevel(value: number): string {
    switch (value) {
      case 1:
        return "Gerente";
      case 2:
        return "Atendente";
      case 3:
        return "Cliente";
      default:
        return "";
    }
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
    if (this.userAccountForm.invalid) {
      return;
    }
    const userAccountCommand: UserAccount = this.getUserAccountCommand();
    this.onSubmit.emit(userAccountCommand);

    //Resets the form.
    this.userAccountForm.reset();
    formDirective.resetForm();
  }

  private getUserAccountCommand(): UserAccount {
    return {
      userAccountID: this.userAccountData ? this.userAccountData.userAccountID : 0,
      name: this.name.value,
      mail: this.mail.value,
      level: this.getAccountLevelId(),
    };
  }

  getAccountLevelId(): number {
    if (this.level.value === 'Gerente')
      return 1;
    if (this.level.value === 'Atendente')
      return 2;
    if (this.level.value === 'Cliente')
      return 3;

    return 0;
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
