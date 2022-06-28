import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CartoesService } from 'src/app/Services/cartoes.service';

@Component({
  selector: 'app-atualizar-cartao',
  templateUrl: './atualizar-cartao.component.html',
  styleUrls: ['../listagem-cartoes/listagem-cartoes.component.css']
})
export class AtualizarCartaoComponent implements OnInit {

  formulario: any;
  cartao: Observable<any>;
  numeroCartao: string;
  cartaoId: number;
  erros: string[];

  constructor(private cartoesService: CartoesService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.erros = [];
    this.cartaoId = this.route.snapshot.params['id'];

    this.cartoesService.PegarCartaoPeloId(this.cartaoId).subscribe(resultado => {
      this.numeroCartao = resultado.numero;
      this.formulario = new FormGroup({
        cartaoId: new FormControl(resultado.cartaoId),
        nome: new FormControl(resultado.nome, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        bandeira: new FormControl(resultado.bandeira, [Validators.required, Validators.minLength(1), Validators.maxLength(15)]),
        numero: new FormControl(resultado.numero, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
        limite: new FormControl(resultado.limite, [Validators.required]),
        usuarioId: new FormControl(resultado.usuarioId)
      });
    })
  }

  get propriedade(){
    return this.formulario.controls;
  }

  VoltarListagem(): void{
    this.router.navigate(['cartoes/listagemcartoes']);
  }
  EnviarFormulario(): void{
    this.erros = [];

    const cartao = this.formulario.value;

    this.cartoesService.AtualizarCartao(this.cartaoId, cartao).subscribe(resultado => {
      this.router.navigate(['cartoes/listagemcartoes']);
      this.snackBar.open(resultado.mensagem, null, {
        duration: 4000,
        horizontalPosition: 'right',
        verticalPosition: 'top',
      });
    });
  }

}
