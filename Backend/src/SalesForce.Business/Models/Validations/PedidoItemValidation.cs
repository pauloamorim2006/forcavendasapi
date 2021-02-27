using FluentValidation;

namespace ERP.Business.Models.Validations
{
    public class PedidoItemValidation : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation()
        {
            RuleFor(f => f.ProdutoId)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Item)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Quantidade)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deverá ser maior que zero");

            RuleFor(f => f.ValorUnitario)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deverá ser maior que zero");

            RuleFor(f => f.ValorTotal)
                .GreaterThan(0).WithMessage("O campo {PropertyName} deverá ser maior que zero");

        }
    }
}
