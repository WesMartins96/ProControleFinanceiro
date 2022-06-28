import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import  decode  from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})

// Service que terá como função proteger as rotas
export class AuthGuardService implements CanActivate {

  constructor(private jwtHelper: JwtHelperService,
    private router: Router) { }

  canActivate(): boolean{
      const token = localStorage.getItem('TokenUsuarioLogado');

      // verificar se existe ou não
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        return true;
      }

      this.router.navigate(['usuarios/loginusuario']);
      return false;
  }

  VerificarAdministrador(): boolean{
    const token = localStorage.getItem('TokenUsuarioLogado');
    const tokenUsuario: any  = decode(token);

    if (tokenUsuario.role === 'Administrador') {
      return true;
    }else{
      return false;
    }
  }
}
