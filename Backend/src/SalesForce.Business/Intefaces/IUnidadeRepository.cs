using ERP.Business.Models;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Intefaces
{
    public interface IUnidadeRepository : IRepository<Unidade>
    {
        Task<ResponseModel<Unidade>> RecuperarTodos(PaginationFilter filter);
        bool JaExiste(Guid id, string sigla);
        bool Encontrou(Guid id);
    }
}
