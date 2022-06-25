using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using ControleFinanceiro.API.ViewModels;
using FluentValidation;

namespace ControleFinanceiro.API.Validacoes
{                                           //Biblioteca FluentValidation para as validações
    public class FuncoesViewModelValidator : AbstractValidator<FuncoesViewModel>
    {
        public FuncoesViewModelValidator()
        {
            RuleFor(f => f.Name)
                    .NotNull().WithMessage("Preencha a função")
                    .NotEmpty().WithMessage("Preencha a função")
                    .MinimumLength(1).WithMessage("Use mais caracteres")
                    .MaximumLength(30).WithMessage("Use menos caracteres");


           RuleFor(f => f.Descricao)
                    .NotNull().WithMessage("Preencha a descrição")
                    .NotEmpty().WithMessage("Preencha a descrição")
                    .MinimumLength(1).WithMessage("Use mais caracteres")
                    .MaximumLength(50).WithMessage("Use menos caracteres");         
        }
    }
}