using SalesForce.Business.Filter;

namespace SalesForce.Business.Tests.Helpers
{
    public static class HelpersDefault
    {
        public static PaginationFilter GerarFiltro()
        {
            return new PaginationFilter(1, 10);
        }
    }
}
