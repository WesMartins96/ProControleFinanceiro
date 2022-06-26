import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DadosLogin } from '../Models/DadosLogin';
import { DadosRegistros } from '../Models/DadosRegistros';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
};


@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  url = 'https://localhost:5001/api/usuarios'

  constructor(private http: HttpClient) { }

  // retornar a foto com o formato correto
  SalvarFoto(formData: any): Observable<any>
  {
    const apiUrl = `${this.url}/SalvarFoto`;
    return this.http.post<any>(apiUrl, formData);
  }

  RegistrarUsuario(dadosResistro: DadosRegistros): Observable<any>
  {
    const apiUrl = `${this.url}/RegistrarUsuario`;
    return this.http.post<DadosRegistros>(apiUrl, dadosResistro);
  }

  LogarUsuario(dadosLogin: DadosLogin): Observable<any>{
    const apiUrl = `${this.url}/LogarUsuario`;
    return this.http.post<DadosRegistros>(apiUrl, dadosLogin);
  }
}
