import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form!: FormGroup;
  constructor(
    private formBuilder: FormBuilder,
  ) { }

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

  onSubmit() {
    //this.authService.signinUser(email, password);
    console.log(`email:${this.mail.value} - password: ${this.password.value}`);
  }

}