import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators, NgForm } from '@angular/forms';
import { firstValueFrom } from 'rxjs';
import { UserAccount } from 'src/app/models/UserAccount';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserAccountService } from 'src/app/services/UserAccountService';

@Component({
  selector: 'app-user-account-profile',
  templateUrl: './user-account-profile.component.html',
  styleUrls: ['./user-account-profile.component.scss']
})
export class UserAccountProfileComponent implements OnInit {

  photoImgSrc!: string;
  userAccountData!: UserAccount;
  userProfileForm!: FormGroup;

  constructor(
    private authUserAccountService: AuthenticationService,
    private userAccountService: UserAccountService) { }

  ngOnInit(): void {
    this.userProfileForm = new FormGroup({
      userAccountID: new FormControl(''),
      name: new FormControl('', [Validators.required]),
      mail: new FormControl('', [Validators.required, Validators.email]),
      photoFile: new FormControl(''),
    });

    this.loadUserAccount();
  }

  async loadUserAccount() {
    const userAccountId = this.authUserAccountService.authorizedUserAccount.userAccountID;

    this.userAccountData = await firstValueFrom(this.userAccountService.getById(userAccountId));
    this.userAccountData.poster = await firstValueFrom(this.userAccountService.getPhotoByUserId(userAccountId));

    if (this.userAccountData && this.userAccountData.poster) {
      this.photoImgSrc = `data:image/png;base64,${this.userAccountData.poster}`;
    }

    this.name.setValue(this.userAccountData.name);
    this.mail.setValue(this.userAccountData.mail);
  }

  get photoFile() {
    return this.userProfileForm.get('photoFile')! as FormControl;
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
