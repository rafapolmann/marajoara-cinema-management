import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      //private accountService: AccountService,
      private alertService: ToastrService
  ) { }

  ngOnInit() {
      this.form = this.formBuilder.group({
          name: new FormControl( '', [Validators.required]),  
          mail: new FormControl('', [Validators.required, Validators.email]),
          password: ['', [Validators.required, Validators.minLength(6)]]
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


  

  onSubmit() {
      this.submitted = true;

      // reset alerts on submit
      //this.alertService.clear();

      // stop here if form is invalid
      if (this.form.invalid) {
          return;
      }

      this.loading = true;
      console.log(JSON.stringify(this.form.value,undefined, ' '));
      // this.accountService.register(this.form.value)
      //     .pipe(first())
      //     .subscribe({
      //         next: () => {
      //             this.alertService.success('Registration successful', { keepAfterRouteChange: true });
      //             this.router.navigate(['../login'], { relativeTo: this.route });
      //         },
      //         error: error => {
      //             this.alertService.error(error);
      //             this.loading = false;
      //         }
      //     });
  }
}