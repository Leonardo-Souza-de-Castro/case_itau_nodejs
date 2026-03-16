import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirmacao.html',
  styleUrl: './confirmacao.css'
})
export class Confirmacao {

  constructor(private dialogRef: MatDialogRef<Confirmacao>) {}

  cancelar(){
    this.dialogRef.close(false);
  }

  confirmar(){
    this.dialogRef.close(true);
  }

}