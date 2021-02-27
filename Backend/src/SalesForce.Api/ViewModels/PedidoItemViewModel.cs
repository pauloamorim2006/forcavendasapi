using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Api.ViewModels
{
    public class PedidoItemViewModel
    {        
        public Guid? Id { get; set; }
        public Guid? PedidoId { get; set; }
        [Required(ErrorMessage = "Item é de preenchimento obrigatório")]
        public int Item { get; set; }
        [Required(ErrorMessage = "Produto é de preenchimento obrigatório")]
        public Guid ProdutoId { get; set; }
        [Range(0.0001, double.PositiveInfinity, ErrorMessage = "Quantidade dever ser maior que 0")]
        public double Quantidade { get; set; }
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Valor Unitário dever ser maior que 0")]
        public double ValorUnitario { get; set; }
        [Range(0, double.PositiveInfinity, ErrorMessage = "Valor Desconto dever ser maior ou igual a 0")]
        public double ValorDesconto { get; set; }
        [Range(0, double.PositiveInfinity, ErrorMessage = "Valor Acréscimo dever ser maior ou igual a 0")]
        public double ValorAcrescimo { get; set; }
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Valor Total dever ser maior que 0")]
        public double ValorTotal { get; set; }
        [ScaffoldColumn(false)]
        public string ProdutoNome { get; set; }
    }
}
