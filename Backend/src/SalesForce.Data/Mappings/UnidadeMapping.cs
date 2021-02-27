using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class UnidadeMapping : IEntityTypeConfiguration<Unidade>
    {
        public void Configure(EntityTypeBuilder<Unidade> builder)
        {
            builder.ToTable("Unidades");

            builder.HasKey(p => p.Id);
            builder.Property(c => c.Descricao).IsRequired().HasColumnType("varchar(100)");
            builder.Property(c => c.Sigla).IsRequired().HasColumnType("varchar(6)");            
        }
    }
}
