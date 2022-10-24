import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router:Router,
  ) { 

    if(this.authService.authorizedUserAccount)
      this.router.navigateByUrl('');
  }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.form = this.formBuilder.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  get mail() {
    return this.form.get('mail')! as FormControl;
  }

  get password() {
    return this.form.get('password')! as FormControl;
  }

  async onSubmit() {
    if(!this.form.valid)
      return;

     const user = await firstValueFrom( this.authService.login(this.mail.value, this.password.value));
     this.router.navigateByUrl('');
    //this.authService.signinUser(email, password);
    // `email:${this.mail.value} - password: ${this.password.value}`);/
  }
  googleLogin(){
    //Todo: facebook login
  }
  facebookLogin(){
    //Todo:facebook login
  }

}
