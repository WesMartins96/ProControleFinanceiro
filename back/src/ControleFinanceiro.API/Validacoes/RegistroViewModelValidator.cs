using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.ViewModels;
using FluentValidation;

namespace ControleFinanceiro.API.Validacoes
{
    public class RegistroViewModelValidator : AbstractValidator<RegistroViewModel>
    {
        public RegistroViewModelValidator()
        {
            // Validações do registro
            RuleFor(r => r.NomeUsuario)
                    .NotNull().WithMessage("Preencha o nome do usuario")
                    .NotEmpty().WithMessage("Preencha o nome do usuario")
                    .MinimumLength(1).WithMessage("Use mais caracteres")
                    .MaximumLength(50).WithMessage("Use menos caracteres");


            RuleFor(r => r.CPF)
                    .NotNull().WithMessage("Preencha o CPF")
                    .NotEmpty().WithMessage("Preencha o CPF")
                    .MinimumLength(1).WithMessage("Use mais caracteres")
                    .MaximumLength(20).WithMessage("Use menos caracteres");    


            RuleFor(r => r.Profissao)
                    .NotNull().WithMessage("Preencha a profissão")
                    .NotEmpty().WithMessage("Preencha a profissão")
                    .MinimumLength(1).WithMessage("Use mais caracteres")
                    .MaximumLength(50).WithMessage("Use menos caracteres");  


            RuleFor(r => r.Foto)
                    .NotNull().WithMessage("Escolha a foto")
                    .NotEmpty().WithMessage("Escolha a foto");         


            RuleFor(r => r.Email)
                    .NotNull().WithMessage("Preencha com o email")
                    .NotEmpty().WithMessage("Preencha com o email")
                    .MinimumLength(10).WithMessage("Use mais caracteres")
                    .MaximumLength(50).WithMessage("Use menos caracteres")
                    .EmailAddress().WithMessage("Email invalido"); 

            RuleFor(r => r.Senha)
                    .NotNull().WithMessage("Preencha a senha")
                    .NotEmpty().WithMessage("Preencha a senha")
                    .MinimumLength(6).WithMessage("Use mais caracteres")
                    .MaximumLength(50).WithMessage("Use menos caracteres");    



        }
    }
}