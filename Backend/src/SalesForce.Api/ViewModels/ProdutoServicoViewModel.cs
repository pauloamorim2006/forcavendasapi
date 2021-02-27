using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Api.ViewModels
{
    public class ProdutoServicoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        [StringLength(100, ErrorMessage = "A {0} deve ser ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        [Display(Name = "Descrição")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Estoque é de preenchimento obrigatório")]
        public double Estoque { get; set; }
        [Required(ErrorMessage = "Valor é de preenchimento obrigatório")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Unidade é de preenchimento obrigatório")]
        [Display(Name = "Unidade")]
        public Guid UnidadeId { get; set; }        
        public bool Ativo { get; set; }
        [Display(Name = "Permitir Fracionar")]
        public bool PermiteFracionar { get; set; }
        public string Gtin { get; set; }
        
        [ScaffoldColumn(false)]
        public string UnidadeSigla { get; set; }
    }
}
