using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class FormaPagamentoMapping : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.ToTable("FormasPagamento");

            builder.HasKey(p => p.Id);
            builder.Property(c => c.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(c => c.Ativo);
            builder.Property(c => c.Tipo);
            builder.Property(c => c.Tef);
            builder.Property(c => c.Credito);
            builder.Property(c => c.PermitirTroco);
            builder.Property(c => c.ConfiguracaoFiscal).IsRequired();            
        }
    }
}
