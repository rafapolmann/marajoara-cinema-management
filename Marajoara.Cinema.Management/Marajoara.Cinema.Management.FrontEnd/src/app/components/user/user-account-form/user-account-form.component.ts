import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserAccount } from 'src/app/models/UserAccount';

@Component({
  selector: 'app-user-account-form',
  templateUrl: './user-account-form.component.html',
  styleUrls: ['./user-account-form.component.scss']
})
export class UserAccountFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<UserAccount>();
  @Output() onCancel = new EventEmitter();
  @Input() userAccountData!: UserAccount;
  constructor() { }

  ngOnInit(): void {
  }

}
