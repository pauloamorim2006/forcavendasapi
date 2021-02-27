using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class ProdutoServicoMapping : IEntityTypeConfiguration<ProdutoServico>
    {
        public void Configure(EntityTypeBuilder<ProdutoServico> builder)
        {
            builder.ToTable("ProdutosServicos");
                       
            builder.HasOne(x => x.Unidade).WithMany().HasForeignKey(x => x.UnidadeId).OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(p => p.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Estoque).IsRequired();
            builder.Property(c => c.Valor).IsRequired();
            builder.Property(c => c.Ativo);
            builder.Property(c => c.PermiteFracionar);
            builder.Property(c => c.CodigoInterno); 
        }
    }
}

