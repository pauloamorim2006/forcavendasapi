using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            //Foreikey            
            builder.HasOne(x => x.Cliente).WithMany().HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.CondicaoPagamento).WithMany().HasForeignKey(x => x.CondicaoPagamentoId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.FormaPagamento).WithMany().HasForeignKey(x => x.FormaPagamentoId).OnDelete(DeleteBehavior.Restrict);            

            //Propriedades
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR PedidoSequencia");
        }
    }
}
