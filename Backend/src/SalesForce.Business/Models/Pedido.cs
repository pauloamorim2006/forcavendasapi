using ERP.Business.Models.Validations;
using System;
using System.Collections.Generic;

namespace ERP.Business.Models
{
    public class Pedido: Entity
    {
        public int Codigo { get; set; }
        public StatusPedido Status { get; set; } = StatusPedido.Aberto;                
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }        
        public DateTime Data { get; set; }
        public Guid CondicaoPagamentoId { get; set; }
        public CondicaoPagamento CondicaoPagamento { get; set; }        
        public Guid FormaPagamentoId { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public List<PedidoItem> PedidoItens { get; set; }
        public override bool EhValido()
        {
            ValidationResult = new PedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
