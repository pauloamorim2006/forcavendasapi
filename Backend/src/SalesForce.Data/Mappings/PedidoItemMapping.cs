using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidosItens");
            //Foreikey            
            builder.HasOne(x => x.Produto).WithMany().HasForeignKey(x => x.ProdutoId).OnDelete(DeleteBehavior.Restrict);            

            //Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Item).IsRequired();
            builder.Property(x => x.Quantidade).IsRequired();
            builder.Property(x => x.ValorUnitario).IsRequired();
            builder.Property(x => x.ValorDesconto).IsRequired();
            builder.Property(x => x.ValorAcrescimo).IsRequired();
            builder.Property(x => x.ValorTotal).IsRequired();            
        }
    }
}
