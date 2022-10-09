import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UserAccount } from 'src/app/models/UserAccount';
import { ToastrService } from 'src/app/services/toastr.service';
import { firstValueFrom } from 'rxjs';
import { UserAccountService } from 'src/app/services/UserAccountService';

@Component({
  selector: 'app-user-account-edit',
  templateUrl: './user-account-edit.component.html',
  styleUrls: ['./user-account-edit.component.scss']
})
export class UserAccountEditComponent implements OnInit {
  userAccountData!: UserAccount;
  constructor(private router: Router,
    private route: ActivatedRoute,
    private userAccountService: UserAccountService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.loadSession(id);
  }

  async loadSession(userAccountId: number) {
    this.userAccountData = await firstValueFrom(this.userAccountService.getById(userAccountId));
  }
  async onSubmit(userAccount: UserAccount) {
    try {
      await firstValueFrom(this.userAccountService.update(userAccount));
    } catch (exception: any) {
      this.toastr.showErrorMessage(`error status ${exception.status}; Message: ${Object.values(exception.error)[0]}`);
    } finally {
      this.navigateToList();
    }
  }

  onCancel() {
    this.navigateToList();
  }

  navigateToList() {
    this.router.navigateByUrl('/users');
  }
}
