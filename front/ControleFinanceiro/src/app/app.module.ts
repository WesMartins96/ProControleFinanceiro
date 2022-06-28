import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';

import { TiposService } from './Services/tipos.service';
import { CategoriasService } from './Services/categorias.service';
import { FuncoesService } from './Services/funcoes.service';
import { MesService } from './Services/mes.service';
import { DespesasService } from './Services/despesas.service';

import { ListagemCategoriasComponent, DialogExclusaoCategoriasComponent } from './components/Categoria/listagem-categorias/listagem-categorias.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { NovaCategoriaComponent } from './components/Categoria/nova-categoria/nova-categoria.component';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDividerModule } from '@angular/material/divider';
import { MatSelectModule } from '@angular/material/select';
import { MatGridListModule } from '@angular/material/grid-list';
import { AtualizarCategoriaComponent } from './components/Categoria/atualizar-categoria/atualizar-categoria.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { DialogExclusaoFuncoesComponent, ListagemFuncoesComponent } from './components/Funcao/listagem-funcoes/listagem-funcoes.component';
import { NovaFuncaoComponent } from './components/Funcao/nova-funcao/nova-funcao.component';
import { AtualizarFuncaoComponent } from './components/Funcao/atualizar-funcao/atualizar-funcao.component';
import { RegistrarUsuarioComponent } from './components/Usuario/Registro/registrar-usuario/registrar-usuario.component';

import { FlexLayoutModule } from '@angular/flex-layout';
import { NgxMaskModule } from 'ngx-mask';
import { LoginUsuarioComponent } from './components/Usuario/Login/login-usuario/login-usuario.component';
import { JwtModule } from '@auth0/angular-jwt';
import { DashboardComponent } from './components/Dashboard/dashboard/dashboard.component';
import { HeaderComponent } from './components/Dashboard/header/header.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AuthGuardService } from './Services/auth-guard.service';
import { CartoesService } from './Services/cartoes.service';
import { NovoCartaoComponent } from './components/Cartao/novo-cartao/novo-cartao.component';
import { DialogExclusaoCartoesComponent, ListagemCartoesComponent } from './components/Cartao/listagem-cartoes/listagem-cartoes.component';
import { AtualizarCartaoComponent } from './components/Cartao/atualizar-cartao/atualizar-cartao.component';
import { NovaDespesaComponent } from './components/Despesa/nova-despesa/nova-despesa.component';

export function PegarTokenUsuario(){
  return localStorage.getItem("TokenUsuarioLogado");
}




@NgModule({
  declarations: [
    AppComponent,
    ListagemCategoriasComponent,
    NovaCategoriaComponent,
    AtualizarCategoriaComponent,
    DialogExclusaoCategoriasComponent,
    ListagemFuncoesComponent,
    NovaFuncaoComponent,
    AtualizarFuncaoComponent,
    DialogExclusaoFuncoesComponent,
    RegistrarUsuarioComponent,
    LoginUsuarioComponent,
    DashboardComponent,
    HeaderComponent,
    NovoCartaoComponent,
    ListagemCartoesComponent,
    AtualizarCartaoComponent,
    DialogExclusaoCartoesComponent,
    NovaDespesaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatDividerModule,
    MatSelectModule,
    MatGridListModule,
    MatDialogModule,
    FormsModule,
    MatAutocompleteModule,
    MatPaginatorModule,
    MatSortModule,
    MatSnackBarModule,
    MatProgressBarModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    FlexLayoutModule,
    NgxMaskModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter: PegarTokenUsuario,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: []
      }
    })

  ],

  providers: [
    TiposService,
    CategoriasService,
    FuncoesService,
    HttpClientModule,
    CartoesService,
    AuthGuardService,
    MesService,
    DespesasService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
