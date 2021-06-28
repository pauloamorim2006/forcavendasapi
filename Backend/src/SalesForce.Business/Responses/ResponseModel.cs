using ERP.Business.Models;
using System.Collections.Generic;

namespace SalesForce.Business.Responses
{
    public class ResponseModel<T>
    {
        private List<Cliente> clientes;

        public ResponseModel(List<Cliente> clientes)
        {
            this.clientes = clientes;
        }

        public ResponseModel(List<T> Data, int Total)
        {
            this.Data = Data;
            this.Total = Total;
        }

        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
