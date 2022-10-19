import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ToastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  form!: FormGroup;

  constructor(
      private formBuilder: FormBuilder,      
      private authService:AuthenticationService,
      private alertService: ToastrService
  ) { }

  ngOnInit() {
      this.form = this.formBuilder.group({
          name: new FormControl( '', [Validators.required]),  
          mail: new FormControl('', [Validators.required, Validators.email]),
          // password: ['', [Validators.required, Validators.minLength(6)]]
      });
  }
  
  get name() {
    return this.form.get('name')! as FormControl;
  }

  get mail() {
    return this.form.get('mail')! as FormControl;
  }

  get password() {
    return this.form.get('password')! as FormControl;
  }


  

  async onSubmit() {      
      if (this.form.invalid) {
          return;
      }
      // console.log(JSON.stringify(this.form.value,undefined, ' '));
      const userId = await firstValueFrom(this.authService.register(this.name.value,this.mail.value));
      this.form.reset();

      this.alertService.showErrorMessage(`${userId}`);
  }
}