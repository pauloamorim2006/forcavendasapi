using SalesForce.Business.Filter;
using System;

namespace SalesForce.Api.Services
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
