using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ERP.Api.ViewModels
{
    public class PedidoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Status do Pedido é de preenchimento obrigatório")]
        public int Status { get; set; }       
        [Required(ErrorMessage = "Cliente é de preenchimento obrigatório")]
        public Guid ClienteId { get; set; }
        [Required(ErrorMessage = "Data do Pedido é de preenchimento obrigatório")]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "Condição de Pagamento é de preenchimento obrigatório")]
        [Display(Name = "Condição Pagamento")]
        public Guid CondicaoPagamentoId { get; set; }
        [Required(ErrorMessage = "Forma de Pagamento é de preenchimento obrigatório")]
        [Display(Name = "Forma Pagamento")]
        public Guid FormaPagamentoId { get; set; }
        public int? Codigo { get; set; }        
        [ScaffoldColumn(false)]
        public string ClienteNome { get; set; }
        [ScaffoldColumn(false)]
        public string FormaPagamentoNome { get; set; }
        [ScaffoldColumn(false)]
        public string CondicaoPagamentoDescricao { get; set; }
        public double? ValorTotal { get => PedidoItens?.Sum(x => x.ValorTotal); }
        public string StatusDescricao
        {
            get =>
                Status == 1 ? "Faturado" :
                Status == 2 ? "Cancelado" :
                "Aberto";
        }
        public List<PedidoItemViewModel> PedidoItens { get; set; }
    }
}
