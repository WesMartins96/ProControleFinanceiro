using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFinanceiro.API.Models;
using FluentValidation;

namespace ControleFinanceiro.API.Validacoes
{
    public class DespesaValidator : AbstractValidator<Despesa>
    {
        public DespesaValidator()
        {
            RuleFor(d => d.CartaoId)
                .NotEmpty().WithMessage("Escolha o cartão")
                .NotNull().WithMessage("Escolha o cartão");

            RuleFor(d => d.Descricao)
                .NotNull().WithMessage("Preencha a descrição")
                .NotEmpty().WithMessage("Preencha a descrição")
                .MinimumLength(1).WithMessage("Use mais caracteres")
                .MaximumLength(50).WithMessage("Use mais caracteres");

            RuleFor(d => d.Valor)
                .NotNull().WithMessage("Preencha o valor")
                .NotEmpty().WithMessage("Preencha o valor")
                //Para o usuario nao digitar um valor negativo
                .InclusiveBetween(0, double.MaxValue).WithMessage("Valor inválido");

            RuleFor(d => d.MesId)
                .NotEmpty().WithMessage("Escolha o mês")
                .NotNull().WithMessage("Escolha o mês");

            RuleFor(d => d.Ano)
               .NotNull().WithMessage("Preencha o ano")
               .NotEmpty().WithMessage("Preencha o ano")
               .InclusiveBetween(2022, 2034).WithMessage("Valor inválido");
        }
    }
}