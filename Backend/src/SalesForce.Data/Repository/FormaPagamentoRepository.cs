using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using SalesForce.Data.Cache;
using System;
using System.Linq;

namespace ERP.Data.Repository
{
    public class FormaPagamentoRepository : Repository<FormaPagamento>, IFormaPagamentoRepository
    {
        public FormaPagamentoRepository(SalesForceDbContext context, ICache cache) : base(context, cache) { }        
        public bool JaExiste(Guid id, string placa)
        {
            return Buscar(f => f.Nome == placa && f.Id != id).Result.Any();
        }
        public bool Encontrou(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();
        }
    }
}
