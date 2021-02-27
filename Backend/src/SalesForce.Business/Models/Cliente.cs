using ERP.Business.Models.Validations;
using System;

namespace ERP.Business.Models
{
    public class Cliente: Entity
    {
        public string Nome { get; set; }
        public string CnpjCpfDi { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public Guid CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public bool Ativo { get; set; }
        public string TipoPessoa { get; set; }
        public string Telefone { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string InscricaoEstadual { get; set; }
        public int TipoInscricaoEstadual { get; set; }
        public bool ConsumidorFinal { get; set; }
        public override bool EhValido()
        {
            ValidationResult = new ClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
