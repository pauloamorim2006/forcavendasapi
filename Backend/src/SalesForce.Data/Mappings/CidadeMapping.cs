using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("Cidades");

            builder.HasKey(p => p.Id);
            builder.Property(c => c.CodigoIbge).IsRequired();
            builder.Property(c => c.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Uf).IsRequired().HasMaxLength(2);            
        }
    }
}
