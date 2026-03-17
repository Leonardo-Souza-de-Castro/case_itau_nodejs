import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Cliente } from '../shared/models/clientes';

@Injectable({
  providedIn: 'root',
})
export class Requests {

  apiUrl = 'http://localhost:5220/api/Cliente';

  private atualizarLista = new Subject<void>();
  atualizarLista$ = this.atualizarLista.asObservable();

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }) 
  };
  
  constructor(
    private httpClient: HttpClient
  ) { }

  notificarAtualizacao() {
    this.atualizarLista.next();
  }

  public listarClientes() {
    return this.httpClient.get<Cliente[]>(this.apiUrl);
  }

  public buscarCliente(id: number){
    return this.httpClient.get<Cliente>(this.apiUrl + "/" + id);
  }

  public cadastrarCliente(cliente: any)  {
    return this.httpClient.post(this.apiUrl, cliente, this.httpOptions);
  }

  public deletarCliente(id: number) {
    return this.httpClient.delete(this.apiUrl + "/" + id);
  }

  public editarCliente(id: number, cliente: any)  {
    return this.httpClient.put(this.apiUrl + "/" + id, cliente, this.httpOptions);
  }

  public sacar(id: number, saldo: any) {
    return this.httpClient.put(this.apiUrl + "/" + id + "/sacar", saldo, this.httpOptions);
  }
  
  public depositar(id: number, saldo: any)  {
    return this.httpClient.put(this.apiUrl + "/" + id + "/depositar", saldo, this.httpOptions);
  }

}
