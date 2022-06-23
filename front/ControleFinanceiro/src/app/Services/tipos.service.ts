import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tipo } from '../Models/Tipo';

@Injectable({
  providedIn: 'root'
})
export class TiposService {

  url: string = 'https://localhost:5001/api/tipos';

  constructor(private http: HttpClient) { }

  PegarTodos() : Observable<Tipo[]>{
    return this.http.get<Tipo[]>(this.url);
  }


}
