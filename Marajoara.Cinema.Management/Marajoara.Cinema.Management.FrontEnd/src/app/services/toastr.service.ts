import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarRef, TextOnlySnackBar } from '@angular/material/snack-bar';

/** @description 
 * Service used for showing toast like notifications; 
 * Uses the MatSnackBar component
 */

@Injectable({
  providedIn: 'root',
})
export class TotastrService {  
  constructor(private snackBar: MatSnackBar) {}

  /**
   * Opens a MatSnackBar with the given message and actionText.
   * @param message Text message to show
   * @param actionText Text of the action to show, set to null or '' to hide the action button. Default: 'Fechar'
   * @returns return MatSackBarRef, can be used to know when the user dismisses it
   */
  showErrorMessage(message:string, actionText:string='Fechar'):MatSnackBarRef<TextOnlySnackBar> {
    return this.snackBar.open(
        message,
        actionText,        
        {
          verticalPosition: 'top',
          horizontalPosition: 'right',
        }
      );
  }
}

