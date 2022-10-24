import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { UserAccountService } from 'src/app/services/UserAccountService';
import { ToastrService } from 'src/app/services/toastr.service';
import { UserAccount } from 'src/app/models/UserAccount';

@Component({
  selector: 'app-user-account-add',
  templateUrl: './user-account-add.component.html',
  styleUrls: ['./user-account-add.component.scss']
})
export class UserAccountAddComponent implements OnInit {

  constructor(
    private userAccountService: UserAccountService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(useraccount: UserAccount) {
     this.saveUserAccount(useraccount);
  }

  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigate(['/users']);
  }

  async saveUserAccount(useraccount: UserAccount) {
    try {
      useraccount.userAccountID = await firstValueFrom(
        this.userAccountService.add(useraccount)
      );
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status} - ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }

}
