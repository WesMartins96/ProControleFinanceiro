import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AtualizarCartaoComponent } from './components/Cartao/atualizar-cartao/atualizar-cartao.component';
import { ListagemCartoesComponent } from './components/Cartao/listagem-cartoes/listagem-cartoes.component';
import { NovoCartaoComponent } from './components/Cartao/novo-cartao/novo-cartao.component';
import { AtualizarCategoriaComponent } from './components/Categoria/atualizar-categoria/atualizar-categoria.component';
import { ListagemCategoriasComponent } from './components/Categoria/listagem-categorias/listagem-categorias.component';
import { NovaCategoriaComponent } from './components/Categoria/nova-categoria/nova-categoria.component';
import { DashboardComponent } from './components/Dashboard/dashboard/dashboard.component';
import { NovaDespesaComponent } from './components/Despesa/nova-despesa/nova-despesa.component';
import { AtualizarFuncaoComponent } from './components/Funcao/atualizar-funcao/atualizar-funcao.component';
import { ListagemFuncoesComponent } from './components/Funcao/listagem-funcoes/listagem-funcoes.component';
import { NovaFuncaoComponent } from './components/Funcao/nova-funcao/nova-funcao.component';
import { LoginUsuarioComponent } from './components/Usuario/Login/login-usuario/login-usuario.component';
import { RegistrarUsuarioComponent } from './components/Usuario/Registro/registrar-usuario/registrar-usuario.component';
import { AuthGuardService } from './Services/auth-guard.service';


const routes: Routes = [
  {
    path: '', component: DashboardComponent,
    canActivate: [AuthGuardService],
    children: [
      {
        path: 'categorias/listagemcategorias', component: ListagemCategoriasComponent
      },
      {
        path: 'categorias/novacategoria', component: NovaCategoriaComponent
      },
      {
        path: 'categorias/atualizarcategoria/:id', component: AtualizarCategoriaComponent
      },
      {
        path: 'funcoes/listagemfuncoes', component: ListagemFuncoesComponent
      },
      {
        path: 'funcoes/novafuncao', component: NovaFuncaoComponent
      },
      {
        path: 'funcoes/atualizarfuncao/:id', component: AtualizarFuncaoComponent
      },
      {
        path: 'cartoes/novocartao', component: NovoCartaoComponent
      },
      {
        path: 'cartoes/listagemcartoes', component: ListagemCartoesComponent
      },
      {
        path: 'cartoes/atualizarcartao/:id', component: AtualizarCartaoComponent
      },
      {
        path: 'despesas/novadespesa', component: NovaDespesaComponent
      },
    ]
  },



  {
    path: 'usuarios/registrarusuario', component: RegistrarUsuarioComponent
  },
  {
    path: 'usuarios/loginusuario', component: LoginUsuarioComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
