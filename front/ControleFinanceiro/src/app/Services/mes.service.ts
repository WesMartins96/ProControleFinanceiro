import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Mes } from '../Models/Mes';

@Injectable({
  providedIn: 'root'
})
export class MesService {

  url = 'https://localhost:5001/api/meses';

  constructor(private http: HttpClient) { }

  //retorna um array de mes com todos os meses para o usuario escolher
  PegarTodos(): Observable<Mes[]>{
    return this.http.get<Mes[]>(this.url);
  }
}
