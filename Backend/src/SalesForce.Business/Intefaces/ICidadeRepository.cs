using ERP.Business.Models;
using System;

namespace ERP.Business.Intefaces
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        bool JaExiste(Guid id, int codigoIbge);
        bool Encontrou(Guid id);
    }
}
