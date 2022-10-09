import { Component, ViewChild, OnInit } from '@angular/core';
import { UserAccount } from 'src/app/models/UserAccount';
import { UserAccountService } from 'src/app/services/UserAccountService';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
import { ToastrService } from 'src/app/services/toastr.service';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-user-account-list',
  templateUrl: './user-account-list.component.html',
  styleUrls: ['./user-account-list.component.scss']
})
export class UserAccountListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'mail', 'level'];
  dataToDisplay: UserAccount[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);
  selectedUserAccountID: number = -1;
  selectedUserAccount!: UserAccount;

  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(
    private userAccountService: UserAccountService,
    private router: Router,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadUserAccounts();
  }

  async loadUserAccounts() {
    this.dataToDisplay = await firstValueFrom(this.userAccountService.getAll());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getUserAccountTypeDescription(userAccountType: Number): string {
    switch (userAccountType) {
      case 1:
        return "Gerente";
      case 2:
        return "Atendente";
      case 3:
        return "Cliente";
      default:
        return "Usuário Inválido";
    }
  }

  applyFilter(event: any): void {
    this.selectedUserAccountID = -1;
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }

  highlight(row: any) {
    this.selectedUserAccountID = row.cineRoomID;
    this.selectedUserAccount = row;
  }

  onAddClick() {
    this.router.navigateByUrl('newuseraccount');
  }

  onEditClick() {
    if (this.selectedUserAccountID === -1)
      return;
    this.router.navigateByUrl(`useraccount/${this.selectedUserAccountID}/edit`);
  }

  async onDeleteClick() {
    if (this.selectedUserAccountID === -1)
      return;

    if (!(await firstValueFrom(this.openDeleteDialog().afterClosed())))
      return;

    try {
      await this.deleteSelected();
    } catch (exception: any) {
      this.toastr.showErrorMessage(
        `error status ${exception.status} - ${Object.values(exception.error)[0]
        }`
      );
    }
  }

  async deleteSelected() {
    if (await firstValueFrom(this.userAccountService.delete(this.selectedUserAccountID))) {
      const index = this.dataSource.data.indexOf(this.selectedUserAccount, 0);

      if (index === -1)
        return;

      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  openDeleteDialog() {
    return this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Exclusão de conta de usuário',
        message: `Deseja mesmo excluir o usuário ${this.selectedUserAccount.name}?`,
        confirmText: 'Excluir',
      },
    });
  }
}
