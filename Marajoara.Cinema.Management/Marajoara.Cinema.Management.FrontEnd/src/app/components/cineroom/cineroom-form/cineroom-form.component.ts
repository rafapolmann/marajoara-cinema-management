import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  FormGroupDirective,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { CineRoom } from 'src/app/models/CineRoom';

@Component({
  selector: 'app-cineroom-form',
  templateUrl: './cineroom-form.component.html',
  styleUrls: ['./cineroom-form.component.scss'],
})
export class CineroomFormComponent implements OnInit {
  @Output() onSubmit = new EventEmitter<CineRoom>();
  @Output() onCancel = new EventEmitter();
  @Input() cineRoomData!: CineRoom;
  cineRoomForm!: FormGroup;
  constructor() {}

  ngOnInit(): void {
    this.cineRoomForm = new FormGroup({
      cineRoomID: new FormControl(
        this.cineRoomData ? this.cineRoomData.cineRoomID : ''
      ),
      name: new FormControl(this.cineRoomData ? this.cineRoomData.name : '', [
        Validators.required,
      ]),
      seatsColumn: new FormControl(
        this.cineRoomData ? this.cineRoomData.seatsColumn : 5,
        [Validators.required]
      ),
      seatsRow: new FormControl(
        this.cineRoomData ? this.cineRoomData.seatsRow : 4,
        [Validators.required, Validators.min(1)]
      ),
      totalSeats: new FormControl(
        this.cineRoomData
          ? this.cineRoomData.seatsRow! * this.cineRoomData.seatsColumn!
          : 20,
        [Validators.required, Validators.min(20), Validators.max(100)]
      ),
    });
  }

  get name() {
    return this.cineRoomForm.get('name')!;
  }

  get seatsColumn() {
    return this.cineRoomForm.get('seatsColumn')!;
  }

  get seatsRow() {
    return this.cineRoomForm.get('seatsRow')!;
  }
  get totalSeats() {
    return this.cineRoomForm.get('totalSeats');
  }

  onSeatCountChange(): void {
    var totSeats = Number(this.seatsColumn.value) * Number(this.seatsRow.value);
    this.totalSeats!.setValue(totSeats);
  }

  cancel() {
    this.onCancel.emit();
  }
  submit(formDirective: FormGroupDirective): void {
    console.log(this.cineRoomForm);
    if (this.cineRoomForm.invalid) {
      return;
    }
    this.onSubmit.emit(this.cineRoomForm.value);
    //Resets the form.
    this.cineRoomForm.reset();
    formDirective.resetForm();
  }
}

export const seatCountValidator: ValidatorFn = (
  control: AbstractControl
): ValidationErrors | null => {
  const cols = Number(control.get('seatsColumn')!.value);
  const rows = Number(control.get('seatsRow')!.value);

  if (cols * rows < 20) return { minSeatCount: true };

  if (cols * rows > 100) return { maxSeatCount: true };

  return null;
};
