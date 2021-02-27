using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Api.ViewModels
{
    public class ClienteViewModel
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "CNPJ/CPF/DI é de preenchimento obrigatório")]
        public string CnpjCpfDi { get; set; }
        [Required(ErrorMessage = "Endereço é de preenchimento obrigatório")]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "Número é de preenchimento obrigatório")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Bairro é de preenchimento obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Cep é de preenchimento obrigatório")]
        public string Cep { get; set; }
        [Required(ErrorMessage = "Cidade é de preenchimento obrigatório")]
        public Guid CidadeId { get; set; }        
        public bool Ativo { get; set; }
        [Required(ErrorMessage = "Tipo de Pessoa é de preenchimento obrigatório")]
        public string TipoPessoa { get; set; }
        public string Telefone { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string InscricaoEstadual { get; set; }
        [Required(ErrorMessage = "Tipo I.E. é de preenchimento obrigatório")]
        public int TipoInscricaoEstadual { get; set; }
        [ScaffoldColumn(false)]
        public string CidadeDescricao { get; set; }
        [ScaffoldColumn(false)]
        public string CidadeUf { get; set; }
        public bool ConsumidorFinal { get; set; }
    }
}
