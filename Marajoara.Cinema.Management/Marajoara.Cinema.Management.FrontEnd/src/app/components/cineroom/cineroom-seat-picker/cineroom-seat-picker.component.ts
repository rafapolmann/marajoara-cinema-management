import { Component, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-cineroom-seat-picker',
  templateUrl: './cineroom-seat-picker.component.html',
  styleUrls: ['./cineroom-seat-picker.component.scss'],
})
export class CineroomSeatPickerComponent implements OnInit {
  @Output() selectedSeat: number =-1;
  @Input() set occupiedSeats(value:number[]){
    this._occupiedSeats = value;
    this.refreshSeats();
  }
  @Input() set columnCount(value: number) {
    this._columnCount = value;
    this.refreshSeats();
  }

  @Input() set rowCount(value: number) {
    this._rowRount = value;
    this.refreshSeats();
  } 
  
  @Input() readonlyMode:boolean=false;
  
  _occupiedSeats!:number[];
  _columnCount: number = 0;
  _rowRount: number = 0;
  _columns!: number[];
  _rows!: number[];

  constructor() {
    this.refreshSeats();
  }

  ngOnInit(): void {}
  
  isSeatDisabled(seat:Number):boolean{
    
    if(this.readonlyMode) return true;
    
    if(this._occupiedSeats!= null && this._occupiedSeats.find(s => s === seat)) return true;
    
    return false;
  }

  onSeatClick(seatNumber: number) {
    this.selectedSeat = seatNumber;
  }

  refreshSeats(): void {
    this.selectedSeat = -1;
    this._columns = Array(this._columnCount)
      .fill(0)
      .map((x, i) => i + 1);
    this._rows = Array(this._rowRount)
      .fill(0)
      .map((x, i) => i + 1);
  }
}
