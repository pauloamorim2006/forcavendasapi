using ERP.Business.Models.Validations;

namespace ERP.Business.Models
{
    public class CondicaoPagamento: Entity
    {
        public string Descricao { get; set; }
        public string Nome { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new CondicaoPagamentoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
