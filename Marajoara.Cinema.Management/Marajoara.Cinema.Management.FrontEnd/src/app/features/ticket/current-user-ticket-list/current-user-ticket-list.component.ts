
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { ConfirmDialogComponent } from 'src/app/components/common/confirm-dialog/confirm-dialog.component';
import { DateTimeCustomFormat } from 'src/app/core/pipes/date-time-custom-format';
import { TicketFlat } from 'src/app/models/Ticket';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TicketService } from 'src/app/services/ticket.service';
import { ToastrService } from 'src/app/services/toastr.service';

@Component({
  selector: 'app-current-user-ticket-list',
  templateUrl: './current-user-ticket-list.component.html',
  styleUrls: ['./current-user-ticket-list.component.scss']
})
export class CurrentUserTicketListComponent implements OnInit {
  displayedColumns: string[] = [
    'purchaseDate',
    'code',
    'sessionMovieTitle',
    'sessionSessionDate',
    'sessionCineRoomName',
    'seatNumber',    
    'used',
    'sessionPrice',
  ];
  dataToDisplay: TicketFlat[] = [];
  dataSource = new MatTableDataSource(this.dataToDisplay);

  selectedTicketID: number = -1;
  selectedTicket!: TicketFlat;
  dateTimeCustom: DateTimeCustomFormat = new DateTimeCustomFormat();

  @ViewChild('paginator') paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private ticketService: TicketService,
    private router: Router,
    private dialog: MatDialog,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadTickets();
  }

  async loadTickets() {
    this.dataToDisplay = await firstValueFrom(this.ticketService.getByCurrentUser());
    this.dataSource = new MatTableDataSource(this.dataToDisplay);
    this.configureSort();
    this.configureFilter();

    this.dataSource.paginator = this.paginator;
  }
  configureFilter(): void {
    this.dataSource.filterPredicate = (data: TicketFlat, filter: string) =>
      this.dateTimeCustom.transform(data.sessionSessionDate).indexOf(filter) != -1 ||
      this.dateTimeCustom.transform(data.purchaseDate).indexOf(filter) != -1 ||
      //this.dateTimeCustom.transform(data.endSession!).indexOf(filter) != -1 ||
      //data.price.toString().indexOf(filter) != -1 ||
      data.userAccountName.toLowerCase().indexOf(filter.toLowerCase()) != -1 ||
      data.code.toLowerCase().indexOf(filter.toLowerCase()) != -1 ||
      data.sessionMovieTitle.toLowerCase().indexOf(filter.toLowerCase()) != -1 ||
      data.sessionCineRoomName.toLowerCase().indexOf(filter.toLowerCase()) != -1;
  }

  configureSort(): void {
    this.dataSource.sort = this.sort;
    // this.dataSource.sortingDataAccessor =(obj:any, property)=>{
    //   switch(property){
    //     case 'movie.Title':return obj.movie.title;
    //     case 'cineRoom.name':return obj.cineRoom.name;
    //     default: return obj[property];        
    //   }
    // }
  }
  formatPrice(price: Number): string {
    return `R$ ${price.toFixed(2)}`;
  }

  highlight(row: any) {
    this.selectedTicketID = row.ticketID;
    this.selectedTicket = row;
  }

  applyFilter(event: any): void {
    this.selectedTicketID = -1;
    this.dataSource.filter = event.target.value.trim().toLocaleLowerCase();
  }

  onAddClick() {
    this.router.navigateByUrl('newticket');
  }

  onEditClick() {
    if (this.selectedTicketID === -1) return;
    this.router.navigateByUrl(`ticket/${this.selectedTicketID}/edit`);
  }

  async onDeleteClick() {
    if (this.selectedTicketID === -1) return;

    if (!(await firstValueFrom(this.openDeleteDialog().afterClosed()))) return;

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
    if (
      await firstValueFrom(this.ticketService.delete(this.selectedTicketID))
    ) {
      const index = this.dataSource.data.indexOf(this.selectedTicket, 0);
      if (index === -1) return;

      this.dataSource.data.splice(index, 1);
      this.dataSource._updateChangeSubscription();
    }
  }

  openDeleteDialog() {
    return this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Exclusão de ticket',
        message: `Deseja mesmo excluir o com código ${this.selectedTicket.code}?`,
        confirmText: 'Excluir',
      },
    });

  }
}

