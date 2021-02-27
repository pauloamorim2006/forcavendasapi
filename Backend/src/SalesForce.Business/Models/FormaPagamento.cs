using ERP.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Business.Models
{
    public class FormaPagamento: Entity
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        // D - Dinheiro, H - Cheque, C - Cartao, V - Vale, R - Crediario, O - Outros 
        public string Tipo { get; set; }        
        public bool Tef { get; set; }
        public bool Credito { get; set; }
        public bool PermitirTroco { get; set; }        
        public string ConfiguracaoFiscal { get; set; }
        public override bool EhValido()
        {
            ValidationResult = new FormaPagamentoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
