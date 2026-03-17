import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { Editar } from '../editar/editar';
import { Operacao } from '../operacao/operacao';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { Cliente } from '../../shared/models/clientes';

@Component({
  selector: 'app-opcoes',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatDialogModule, MatButtonModule],
  templateUrl: './opcoes.html',
  styleUrl: './opcoes.css',
})
export class Opcoes {

  clienteSelecionado: Cliente | null = null;

  constructor(@Inject(MAT_DIALOG_DATA) public cliente: Cliente, private dialog: MatDialog, public dialogRef: MatDialogRef<Opcoes>) {}

  ngOnInit() {
    this.clienteSelecionado = this.cliente;
  }

  editar() {
  this.dialogRef.close(true);
  
  this.dialog.open(Editar, {
    data: this.clienteSelecionado,
    autoFocus: true, // Garante foco no novo modal
  });
}

saldo() {
    this.dialogRef.close(true);
    this.dialog.open(Operacao, {
      data: this.clienteSelecionado
    });
  }
}