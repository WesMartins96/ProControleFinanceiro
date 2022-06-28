import { Component, Inject, OnInit, ViewChild} from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA, } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { map, Observable, startWith } from 'rxjs';
import { CartoesService } from 'src/app/Services/cartoes.service';

@Component({
  selector: 'app-listagem-cartoes',
  templateUrl: './listagem-cartoes.component.html',
  styleUrls: ['./listagem-cartoes.component.css'],
})
export class ListagemCartoesComponent implements OnInit {

  cartoes = new MatTableDataSource<any>();
  displayedColumns: string[];
  usuarioId: string = localStorage.getItem('UsuarioId');

  opcoesNumeros: string[] = [];
  autoCompleteInput = new FormControl();
  numeroCartoes: Observable<string[]>;

  //paginação e ordenação
  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  constructor(private cartoesService: CartoesService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.cartoesService.PegarCartoesPeloUsuarioId(this.usuarioId).subscribe( resultado => {

      resultado.forEach(numero => {
        this.opcoesNumeros.push(numero.numero);
      });

      this.cartoes.data = resultado;
      this.cartoes.paginator = this.paginator;
      this.cartoes.sort = this.sort;
    });


    this.displayedColumns = this.ExibirColunas();

    this.numeroCartoes = this.autoCompleteInput.valueChanges.pipe(startWith(''), map(numero => this.FiltrarCartoes(numero)));
  }


  ExibirColunas(): string[]{
    return ['nome', 'bandeira', 'numero', 'limite', 'acoes'];
  }

  FiltrarCartoes(numero: string): string[]{
    if (numero.trim().length >= 4) {
      this.cartoesService.FiltrarCartoes(numero).subscribe(resultado => {
        this.cartoes.data = resultado;
      });
    }
    else{
      if (numero === '') {
        this.cartoesService.PegarCartoesPeloUsuarioId(this.usuarioId).subscribe(resultado => {
          this.cartoes.data = resultado;
        });
      }
    }

    // filtro do que será mostrado para o usuario
    return this.opcoesNumeros.filter(nc => nc.toLowerCase().includes(numero.toLowerCase()))
  }

  AbrirDialog(cartaoId, numero): void {
    this.dialog
      .open(DialogExclusaoCartoesComponent, {
        data: {
          cartaoId: cartaoId,
          numero: numero,
        },
      })
      .afterClosed()
      .subscribe((resultado) => {
        if (resultado === true) {
          this.cartoesService
            .PegarCartoesPeloUsuarioId(this.usuarioId)
            .subscribe((dados) => {
              this.cartoes.data = dados;
              this.cartoes.paginator = this.paginator;
            });
          this.displayedColumns = this.ExibirColunas();
        }
      });
  }
}


@Component({
  selector: 'app-dialog-exclusao-cartoes',
  templateUrl: 'dialog-exclusao-cartoes.html',
})
export class DialogExclusaoCartoesComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private cartoesService: CartoesService,
    private snackBar: MatSnackBar
  ) {}

  ExcluirCartao(cartaoId): void {
    this.cartoesService.ExcluirCartao(cartaoId).subscribe((resultado) => {
      this.snackBar.open(resultado.mensagem, null, {
        duration: 2000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
      });
    });
  }
}


