using ERP.Business.Models;
using System;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<Empresa> Buscar();
        bool JaExiste(Guid id, string cnpjCpfDi);
        bool Encontrou(Guid id);
    }
}
