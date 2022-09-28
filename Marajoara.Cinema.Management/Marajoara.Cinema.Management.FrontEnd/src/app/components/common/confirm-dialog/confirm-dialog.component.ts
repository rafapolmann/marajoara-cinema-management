import { Component, OnInit,Input } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
  @Input() DialogTitle:string =''
  @Input() DialogMessage:string =''  
  
  constructor(public dialogRef: MatDialogRef<ConfirmDialogComponent>) { }

  ngOnInit(): void {
  }
  onConfirm(){
    this.dialogRef.close(true);
  }
  onCancel(){
    this.dialogRef.close(false);
  }

}
