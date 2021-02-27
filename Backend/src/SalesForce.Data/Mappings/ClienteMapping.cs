using ERP.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasOne(x => x.Cidade).WithMany().HasForeignKey(x => x.CidadeId).OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(p => p.Id);
            builder.Property(c => c.Nome).IsRequired().HasMaxLength(60);
            builder.Property(c => c.CnpjCpfDi).IsRequired();
            builder.Property(c => c.Endereco).IsRequired().HasMaxLength(60);
            builder.Property(c => c.Numero).IsRequired().HasMaxLength(60);
            builder.Property(c => c.Bairro).IsRequired().HasMaxLength(60);
            builder.Property(c => c.Cep);
            builder.Property(c => c.Ativo);
            builder.Property(c => c.TipoPessoa).IsRequired();
            builder.Property(c => c.Telefone);
            builder.Property(c => c.Complemento);
            builder.Property(c => c.Email);
            builder.Property(c => c.InscricaoEstadual);
            builder.Property(c => c.TipoInscricaoEstadual).IsRequired();
            builder.Property(c => c.ConsumidorFinal);            
        }
    }
}
