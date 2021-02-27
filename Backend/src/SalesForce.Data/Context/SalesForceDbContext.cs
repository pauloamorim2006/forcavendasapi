using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ERP.Business.Models;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data.Context
{
    public class SalesForceDbContext : DbContext
    {
        public SalesForceDbContext(DbContextOptions<SalesForceDbContext> options) : base(options) { }        
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<ProdutoServico> ProdutosServicos { get; set; }        
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CondicaoPagamento> CondicoesPagamento { get; set; }        
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesForceDbContext).Assembly);

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.HasSequence<int>("PedidoSequencia").StartsAt(1).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}