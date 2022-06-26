import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DadosRegistros } from 'src/app/Models/DadosRegistros';
import { UsuariosService } from 'src/app/Services/usuarios.service';

@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.css']
})
export class RegistrarUsuarioComponent implements OnInit {

  formulario: any;
  foto: File = null;
  erros: string[];

  constructor(private usuariosService: UsuariosService,
    private router: Router) { }

  ngOnInit(): void {
    this.erros = [];

    this.formulario = new FormGroup({
      nomeusuario: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)]),
      cpf: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      profissao: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(30)]),
      foto: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email, Validators.minLength(10), Validators.maxLength(50)]),
      senha: new FormControl(null, [Validators.required, Validators.minLength(6), Validators.maxLength(50)])
    });
  }

  // funcao para pegar as propriedades
  get propriedade(){
    return this.formulario.controls;
  }

  SelecionarFoto(fileInput: any): void{
    this.foto = fileInput.target.files[0] as File;
    const reader = new FileReader();
    reader.onload = function(e : any){
      document.getElementById('foto').removeAttribute('hidden');
      document.getElementById('foto').setAttribute('src', e.target.result);
    }

    reader.readAsDataURL(this.foto);
  }

  EnviarFormulario(): void{
    this.erros = [];
    const usuario = this.formulario.value;

    // para enviar a foto ao back
    const formData: FormData = new FormData();

    if (this.foto != null) {
      formData.append('file', this.foto, this.foto.name);
    }

    //salvar os dados
    this.usuariosService.SalvarFoto(formData).subscribe(resultado => {
      const dadosRegistro: DadosRegistros = new DadosRegistros();
      dadosRegistro.nomeusuario = usuario.nomeusuario;
      dadosRegistro.cpf = usuario.cpf;
      dadosRegistro.foto = resultado.foto; // vem direto do UsuariosController
      dadosRegistro.profissao = usuario.profissao;
      dadosRegistro.email = usuario.email;
      dadosRegistro.senha = usuario.senha;

      this.usuariosService.RegistrarUsuario(dadosRegistro).subscribe(dados => {
        // Armazenar o email do usuario logado
        const emailUsuarioLogado = dados.emailUsuarioLogado;

        const usuarioId = dados.usuarioId;

        const tokenUsuarioLogado = dados.tokenUsuarioLogado;

        // local storage - para utilizar em todo o sistema
        localStorage.setItem('EmailUsuarioLogado', emailUsuarioLogado);
        localStorage.setItem('UsuarioId', usuarioId);
        localStorage.setItem('TokenUsuarioLogado', tokenUsuarioLogado)
        this.router.navigate(['categorias/listagemcategorias']);
      },
      (err) => {
        if(err.status === 400){
          for(const campo in err.error.errors){
            if(err.error.errors.hasOwnProperty(campo)){
              this.erros.push(err.error.errors[campo]);
            }
          }
        }
      });
    });
  }


}
