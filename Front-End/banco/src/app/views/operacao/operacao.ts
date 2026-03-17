import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Cliente } from '../../shared/models/clientes';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Requests } from '../../services/requests';
import { MatIconModule } from '@angular/material/icon';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-operacao',
  imports: [CommonModule, ReactiveFormsModule, MatIconModule],
  templateUrl: './operacao.html',
  styleUrl: './operacao.css',
})

export class Operacao implements OnInit {
  mostrarSaldo = false;
  erroMensagem: string = '';
  formulario!: FormGroup;
  private service = inject(Requests);
  public cliente: Cliente = inject(MAT_DIALOG_DATA);
  private cdr = inject(ChangeDetectorRef);
  public dialogRef: MatDialogRef<Operacao> = inject(MatDialogRef<Operacao>);

  ngOnInit(): void {
    this.formulario = new FormGroup({
      valor: new FormControl(0, [Validators.required, Validators.min(0)])
    });
  }

  cancelar(){
    this.dialogRef.close(true);
  }

  sacar(){
    if (this.formulario.valid) {
      this.service.sacar(this.cliente.id, this.formulario.value).subscribe({
      next: () => {
        this.service.notificarAtualizacao();
        this.dialogRef.close(true);
      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
        this.erroMensagem = "Erro ao realizar saque. Por favor, tente novamente.";
        this.cdr.detectChanges();
      }
    });
    }else{
      this.erroMensagem = "Por favor, insira um valor válido para saque.";
    }
  }

  depositar(){
    if (this.formulario.valid) {
      this.service.depositar(this.cliente.id, this.formulario.value).subscribe({
      next: () => {
        this.service.notificarAtualizacao();
        this.dialogRef.close(true);
      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
        this.erroMensagem = "Erro ao realizar depósito. Por favor, tente novamente.";
        this.cdr.detectChanges();
      }
    });
    }else{
      this.erroMensagem = "Por favor, insira um valor válido para depósito.";
    }
  }
}
