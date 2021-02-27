using FluentValidation;

namespace ERP.Business.Models.Validations
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(f => f.Data)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.ClienteId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.CondicaoPagamentoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.FormaPagamentoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleForEach(x => x.PedidoItens).SetValidator(new PedidoItemValidation());
        }
    }
}
