import { Component, OnInit } from '@angular/core';
import { Requests } from '../../services/requests';
import { Cliente } from '../../shared/models/clientes';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { Confirmacao } from '../confirmacao/confirmacao';
import { Opcoes } from '../opcoes/opcoes';
import { MatDialog } from '@angular/material/dialog';
import { ChangeDetectorRef } from '@angular/core';
import { Cadastrar } from '../cadastrar/cadastrar';

@Component({
  selector: 'app-home',
  imports: [CommonModule, MatIconModule, MatDialogModule, MatButtonModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  clientes: Cliente[] = [];
  clienteSelecionado: Cliente | null = null;
  
  constructor(private service: Requests,
    private dialog: MatDialog,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.listarClientes();

    this.service.atualizarLista$.subscribe(() => {
      setTimeout(() => {
        this.listarClientes();
      }, 200);
    });

  }

  listarClientes(){
    this.service.listarClientes().subscribe({
      next: (dados) => {
        this.clientes = dados;
        this.cdr.detectChanges();
      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
      }
    });
  }

  deletarCliente(id: number){

    const dialogRef = this.dialog.open(Confirmacao);
    dialogRef.afterClosed().subscribe(result => {
      if(result){
        this.service.deletarCliente(id).subscribe(() => {
          this.clientes = this.clientes.filter(c => c.id !== id);
          this.service.notificarAtualizacao();
        });
      }
    });
  }

  abrirInfo(id: number){
    this.service.buscarCliente(id).subscribe({
      next: (dados) => {
        this.clienteSelecionado = dados;
        this.dialog.open(Opcoes, {
          data: this.clienteSelecionado
        });

      },
      error: (erro) => {
        console.error("Erro ao buscar clientes", erro);
      }
    });
  }

  abrirCadastro(){
    const dialogRef = this.dialog.open(Cadastrar);
  }
}
