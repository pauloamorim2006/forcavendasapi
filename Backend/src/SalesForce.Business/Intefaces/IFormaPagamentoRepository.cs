using ERP.Business.Models;
using System;

namespace ERP.Business.Intefaces
{
    public interface IFormaPagamentoRepository : IRepository<FormaPagamento>
    {
        bool JaExiste(Guid id, string nome);
        bool Encontrou(Guid id);
    }
}
