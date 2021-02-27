using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Api.ViewModels
{
    public class FormaPagamentoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        // D - Dinheiro, H - Cheque, C - Cartao, V - Vale, R - Crediario, O - Outros 
        public string Tipo { get; set; }        
        public bool Tef { get; set; }
        public bool Credito { get; set; }
        public bool PermitirTroco { get; set; }
        [Required(ErrorMessage = "Configuração Fiscal é de preenchimento obrigatório")]
        public string ConfiguracaoFiscal { get; set; }        
    }
}
