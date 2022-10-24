import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss'],
})
export class ConfirmDialogComponent implements OnInit {
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public params: {
      title: string;
      message: string;
      cancelText: string;
      confirmText: string;
      hideCancel: boolean;
    },
    public dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {}

  ngOnInit(): void {    
    if (!this.params) this.dialogRef.close(false);
    if (!this.params.title) this.params.title = 'Título';
    if (!this.params.message) this.params.message = 'Mensagem';
    if (!this.params.cancelText) this.params.cancelText = 'Cancelar';
    if (!this.params.confirmText) this.params.confirmText = 'Ok';    
  }
  onConfirm() {
    this.dialogRef.close(true);
  }
  onCancel() {
    this.dialogRef.close(false);
  }
}
