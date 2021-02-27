using FluentValidation;

namespace ERP.Business.Models.Validations
{
    public class ProdutoServicoValidation : AbstractValidator<ProdutoServico>
    {
        public ProdutoServicoValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Estoque)
                    .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Valor)
                    .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.UnidadeId)
                    .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }        
    }
}
