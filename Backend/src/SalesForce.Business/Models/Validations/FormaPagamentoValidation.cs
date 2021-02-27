using FluentValidation;

namespace ERP.Business.Models.Validations
{
    public class FormaPagamentoValidation : AbstractValidator<FormaPagamento>
    {
        public FormaPagamentoValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.ConfiguracaoFiscal)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
