import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ToastrService } from 'src/app/services/toastr.service';
import { UserAccountService } from 'src/app/services/UserAccountService';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { UserAccountChangePassword } from 'src/app/models/UserAccount';
import { FormValidations } from 'src/app/services/form-validations.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm!: FormGroup;
  userAccountID!: number;
  userMail!: string;
  constructor(
    private toastr: ToastrService,
    private router: Router,
    private authUserAccountService: AuthenticationService,
    private userAccountService: UserAccountService
  ) { }

  ngOnInit(): void {
    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl('', [Validators.required]),
      newPassword: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required, FormValidations.equalsTo('newPassword')]),
    });

    this.userAccountID = this.authUserAccountService.authorizedUserAccount.userAccountID;
    this.userMail = this.authUserAccountService.authorizedUserAccount.mail;
  }

  get oldPassword() {
    return this.changePasswordForm.get('oldPassword')! as FormControl;
  }

  get newPassword() {
    return this.changePasswordForm.get('newPassword')! as FormControl;
  }

  get confirmPassword() {
    return this.changePasswordForm.get('confirmPassword')! as FormControl;
  }

  submit(formDirective: FormGroupDirective): void {
    if (this.changePasswordForm.invalid) {
      return;
    }

    try {
      this.updateUserAccountPassword();
      this.router.navigateByUrl('/in-theater');
    } catch (exception: any) {
      console.log(JSON.stringify(exception, undefined, " "));

      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
    } finally {
      this.changePasswordForm.reset();
      formDirective.resetForm();
    }
  }

  async updateUserAccountPassword() {
    const changePasswordCommand: UserAccountChangePassword = this.getChangePasswordCommand();
    await firstValueFrom(this.userAccountService.changePassword(changePasswordCommand));
  }

  getChangePasswordCommand(): UserAccountChangePassword {
    return {
      userAccountID: this.userAccountID,
      mail: this.userMail,
      password: this.oldPassword.value,
      newPassword: this.newPassword.value
    };
  }

  cancel() {
    this.router.navigateByUrl('/in-theater');
  }
}
