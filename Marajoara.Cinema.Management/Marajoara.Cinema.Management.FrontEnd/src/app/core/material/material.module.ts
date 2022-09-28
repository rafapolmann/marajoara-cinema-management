import { NgModule } from '@angular/core';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';

const MODULES = [
  MatNativeDateModule,
  MatSidenavModule,
  MatToolbarModule,
  MatNativeDateModule,
  MatIconModule,
  MatListModule,
  MatTableModule,
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatPaginatorModule,
  MatCheckboxModule,
  MatDialogModule,
];
/**teste */
@NgModule({
  imports: [MODULES],
  exports: [MODULES],
})

export class MaterialModule {}
