using ERP.Business.Models;
using System;

namespace ERP.Business.Intefaces
{
    public interface ICondicaoPagamentoRepository : IRepository<CondicaoPagamento>
    {
        bool JaExiste(Guid id, string placa);
        bool Encontrou(Guid id);
    }
}
