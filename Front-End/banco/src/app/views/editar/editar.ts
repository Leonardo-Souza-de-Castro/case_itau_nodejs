import { Component, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Cliente } from '../../shared/models/clientes';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Requests } from '../../services/requests';


@Component({
  selector: 'app-editar',
  templateUrl: './editar.html',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  styleUrl: './editar.css'
})
export class Editar implements OnInit{

  formulario!: FormGroup;
  private service = inject(Requests);
  private dialog = inject(MatDialog);
  public cliente: Cliente = inject(MAT_DIALOG_DATA);

  ngOnInit(): void {
    this.formulario = new FormGroup({
      nome: new FormControl(this.cliente.nome),
      email: new FormControl(this.cliente.email)
    });
  }

  cancelar(){
    this.dialog.closeAll();
  }

  confirmar(id: number){
    console.log(this.formulario.value);
    this.service.editarCliente(id, this.formulario.value).subscribe({
      next: () => {
        console.log("Cliente editado com sucesso");
        this.dialog.closeAll();
      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
      }
    });
  }

}