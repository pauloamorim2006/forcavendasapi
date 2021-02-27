using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Business.Models.Validations
{
    public class CidadeValidation : AbstractValidator<Cidade>
    {
        public CidadeValidation()
        {
            RuleFor(f => f.CodigoIbge)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(1, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Uf)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 2)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
