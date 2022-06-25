import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { map, Observable, startWith } from 'rxjs';
import { FuncoesService } from 'src/app/Services/funcoes.service';

@Component({
  selector: 'app-listagem-funcoes',
  templateUrl: './listagem-funcoes.component.html',
  styleUrls: ['./listagem-funcoes.component.css']
})
export class ListagemFuncoesComponent implements OnInit {

  funcoes = new MatTableDataSource<any>();
  displayedColumns: string[];

  autocompleteInput = new FormControl();
  opcoesFuncoes: string[] = [];
  nomesFuncoes: Observable<string[]>;

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  constructor(private funcoesService: FuncoesService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.funcoesService.PegarTodos().subscribe((resultado) => {
      resultado.forEach(funcao => {
        this.opcoesFuncoes.push(funcao.name);
      });

      this.funcoes.data = resultado;
      this.funcoes.sort = this.sort;
      this.funcoes.paginator = this.paginator;
    });

    this.displayedColumns = this.ExibirColunas();

    this.nomesFuncoes = this.autocompleteInput.valueChanges.pipe(startWith(''), map(nome => this.FiltrarNomes(nome)))
  }

  ExibirColunas(): string[]{
    return ['nome', 'descricao', 'acoes' ];
  }

  FiltrarNomes(nome: string): string[]{
    if(nome.trim().length >= 4)
    {
      this.funcoesService.FiltrarFuncoes(nome.toLowerCase()).subscribe(resultado => {
        this.funcoes.data = resultado;
      });
    }
    else{
      if(nome === '')
      {
        this.funcoesService.PegarTodos().subscribe((resultado) => {
          this.funcoes.data = resultado;
        });
      }
    }

    return this.opcoesFuncoes.filter(funcao => funcao.toLowerCase().includes(nome.toLowerCase()));
  }

  AbrirDialog(funcaoId, nome): void{
    this.dialog.open(DialogExclusaoFuncoesComponent, {
      data:
      {
        funcaoId : funcaoId,
        nome : nome
      },
    }).afterClosed().subscribe(resultado => {
      if(resultado === true){
        this.funcoesService.PegarTodos().subscribe(dados => {
          this.funcoes.data = dados;
          this.funcoes.paginator = this.paginator;
        });
        
        this.displayedColumns = this.ExibirColunas();
      }
    });
  }
}

@Component({
  selector: 'app-dialog-exclusao-funcoes',
  templateUrl: 'dialog-exclusao-funcoes.html',
})
export class DialogExclusaoFuncoesComponent{
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
  private funcoesService: FuncoesService,
  private snackBar: MatSnackBar){}


  ExcluirFuncao(funcaoId): void{
    this.funcoesService.ExcluirFuncao(funcaoId).subscribe(resultado => {
      this.snackBar.open(resultado.mensagem, null, {
        duration: 4000,
        horizontalPosition: 'right',
        verticalPosition: 'top'
      });
    });
  }
}
