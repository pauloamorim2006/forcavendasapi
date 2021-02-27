using ERP.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Business.Models
{
    public class Empresa: Entity
    {
        public string Nome { get; set; }
        public string Fantasia { get; set; }
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
        public bool Padrao { get; set; }
        public int Crt { get; set; }
        public override bool EhValido()
        {
            ValidationResult = new EmpresaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
