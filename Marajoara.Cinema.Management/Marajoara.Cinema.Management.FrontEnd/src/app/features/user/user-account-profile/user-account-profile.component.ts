import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { UserAccount } from 'src/app/models/UserAccount';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ToastrService } from 'src/app/services/toastr.service';
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
  shouldUpdatePhoto: boolean = false;

  constructor(
    private toastr: ToastrService,
    private router: Router,
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
    this.userAccountData.photo = await firstValueFrom(this.userAccountService.getPhotoByUserId(userAccountId));

    if (this.userAccountData && this.userAccountData.photo) {
      this.photoImgSrc = `data:image/png;base64,${this.userAccountData.photo}`;
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
    try {
      this.updateUserAccountData();
      this.authUserAccountService.updateUserName(this.name.value);
      this.router.navigateByUrl('/in-theater');
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
      this.authUserAccountService.logout();
    } finally {
      this.userProfileForm.reset();
      formDirective.resetForm();
    }
  }

  async updateUserAccountData() {
    this.userAccountData.name = this.name.value;
    await firstValueFrom(this.userAccountService.update(this.userAccountData));
    if (this.userAccountData.photoFile && this.shouldUpdatePhoto) {
      await firstValueFrom(this.userAccountService.updatePhoto(this.userAccountData));
    } else if (this.shouldUpdatePhoto) {
      await firstValueFrom(this.userAccountService.deletePhoto(this.userAccountData.userAccountID));
    }
  }

  cancel() {
    this.router.navigateByUrl('/in-theater');
  }

  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    this.userAccountData.photoFile = file;
    this.userProfileForm.patchValue({ photoFile: file });

    const reader = new FileReader();
    reader.onload = e => this.photoImgSrc = String(reader.result);

    reader.readAsDataURL(file);
    this.shouldUpdatePhoto = true;
  }

  onCleanFile() {
    this.userProfileForm.patchValue({ photoFile: null });
    this.photoImgSrc = '';
    this.shouldUpdatePhoto = true;
  }
}
