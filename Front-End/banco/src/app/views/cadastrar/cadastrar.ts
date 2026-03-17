import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Cliente } from '../../shared/models/clientes';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Requests } from '../../services/requests';

@Component({
  selector: 'app-cadastrar',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './cadastrar.html',
  styleUrl: './cadastrar.css',
})
export class Cadastrar implements OnInit{

  private service = inject(Requests);
  private dialog = inject(MatDialog);
  public cliente: Cliente = inject(MAT_DIALOG_DATA);
  public dialogRef: MatDialogRef<Cadastrar> = inject(MatDialogRef<Cadastrar>);
  erroMensagem: string = '';
  formulario!: FormGroup;

  ngOnInit(): void {
    this.formulario = new FormGroup({
    nome: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email])
  });
  }


  cancelar(){
    this.dialog.closeAll();
  }

  cadastrar(){

    if(this.formulario.invalid){
      this.erroMensagem = "Valor inválido. Por favor, preencha os campos corretamente.";
      return;
    } 
    console.log(this.formulario.value);
    this.service.cadastrarCliente( this.formulario.value).subscribe({
      next: () => {
        this.service.notificarAtualizacao();
        this.dialogRef.close(true);
      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
        this.erroMensagem = "Erro ao cadastrar cliente. Por favor, tente novamente.";
      }
    });
  }
}
