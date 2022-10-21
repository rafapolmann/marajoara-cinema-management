import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators, NgForm } from '@angular/forms';
import { UserAccount } from 'src/app/models/UserAccount';

@Component({
  selector: 'app-user-account-profile',
  templateUrl: './user-account-profile.component.html',
  styleUrls: ['./user-account-profile.component.scss']
})
export class UserAccountProfileComponent implements OnInit {

  photoImgSrc!: string;

  @Input() userAccountData!: UserAccount;
  userProfileForm!: FormGroup;
  constructor() { }

  ngOnInit(): void {
    this.userProfileForm = new FormGroup({
      userAccountID: new FormControl(this.userAccountData ? this.userAccountData.userAccountID : ''),
      name: new FormControl(this.userAccountData ? this.userAccountData.name : '', [Validators.required]),
      mail: new FormControl(this.userAccountData ? this.userAccountData.mail : '', [Validators.required, Validators.email]),
      //level: new FormControl(this.userAccountData ? this.getAccountLevel(this.userAccountData.level) : '', [Validators.required]),
      photoFile: new FormControl(''),
    });
  }

  get name() {
    return this.userProfileForm.get('name')! as FormControl;
  }

  get mail() {
    return this.userProfileForm.get('mail')! as FormControl;
  }

  submit(formDirective: FormGroupDirective): void {
    if (this.userProfileForm.invalid) {
      return;
    }
    // const userAccountCommand: UserAccount = this.getUserAccountCommand();
    // this.onSubmit.emit(userAccountCommand);

    //Resets the form.
    this.userProfileForm.reset();
    formDirective.resetForm();
  }

  cancel() {

  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];

    this.userProfileForm.patchValue({ posterFile: file });

    const reader = new FileReader();
    reader.onload = e => this.photoImgSrc = String(reader.result);

    reader.readAsDataURL(file);
  }

  onCleanFile() {
    this.userProfileForm.patchValue({ posterFile: null });
    this.photoImgSrc = '';
  }

}
