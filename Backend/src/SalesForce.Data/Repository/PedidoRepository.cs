using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace ERP.Data.Repository
{
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(SalesForceDbContext context) : base(context) { }

        public async Task<int> RecuperarQuantidade()
        {
            return await Db.Pedidos.AsNoTracking().CountAsync();
        }

        public async Task<List<Pedido>> RecuperarTodos(
            DateTime? DataInicial,
            DateTime? DataFinal,
            Guid? ClienteId,
            int? Status)
        {
            var predicate = PredicateBuilder.New<Pedido>(true);
            if (DataInicial.HasValue && DataFinal.HasValue)
            {
                if ((DataInicial > DateTime.Now.AddYears(-10)) &&
                    (DataFinal > DateTime.Now.AddYears(-10)) &&
                    (DataFinal >= DataInicial))
                {
                    DateTime dataInicial =
                        new DateTime(
                        DataInicial.GetValueOrDefault().Year,
                        DataInicial.GetValueOrDefault().Month,
                        DataInicial.GetValueOrDefault().Day,
                        0,
                        0,
                        0);
                    DateTime dataFinal =
                        new DateTime(
                        DataFinal.GetValueOrDefault().Year,
                        DataFinal.GetValueOrDefault().Month,
                        DataFinal.GetValueOrDefault().Day,
                        23,
                        59,
                        59);
                    predicate = predicate.And(n => n.Data >= dataInicial && n.Data <= dataFinal);
                }
            }
            else
            {
                DateTime dataInicial =
                    new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    0,
                    0,
                    0);
                DateTime dataFinal =
                    new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    23,
                    59,
                    59);
                predicate = predicate.And(n => n.Data >= dataInicial && n.Data <= dataFinal);
            }
            if (ClienteId.HasValue && ClienteId.GetValueOrDefault(Guid.Empty) != Guid.Empty)
            {
                predicate = predicate.And(n => n.ClienteId == ClienteId);                
            }
            if (Status.HasValue)
            {
                if (Status.GetValueOrDefault() > -1)
                {
                    predicate = predicate.And(n => n.Status == (StatusPedido)Status);
                }
            }

            return await Db.Pedidos.AsNoTracking().
                Include(x => x.Cliente).
                Include(x => x.FormaPagamento).
                Include(x => x.CondicaoPagamento).
                Include(x => x.PedidoItens).
                Where(predicate).
                ToListAsync();
        }

        public async Task<Pedido> RecuperarPorId(Guid id)
        {
            return await Db.Pedidos.
                AsNoTracking().                                          
                Include(x => x.Cliente).
                Include(x => x.FormaPagamento).
                Include(x => x.CondicaoPagamento).
                Include(x => x.PedidoItens).
                    ThenInclude(y => y.Produto).
                Where(x => x.Id == id).
                FirstOrDefaultAsync();
        }

        public async Task Modificar(Pedido pedido)
        {
            var pedidoExistente =
              await Db.Pedidos.
                       Include(x => x.PedidoItens).
                       FirstOrDefaultAsync(x => x.Id == pedido.Id);
            Db.Entry(pedidoExistente).CurrentValues.SetValues(pedido);

            #region Itens
            foreach (var item in pedido.PedidoItens)
            {
                var pedidoItemExistente =
                    pedidoExistente.PedidoItens.FirstOrDefault(p => p.Id == item.Id);

                if (pedidoItemExistente == null)
                {
                    pedidoExistente.PedidoItens.Add(item);
                }
                else
                {
                    Db.Entry(pedidoItemExistente).CurrentValues.SetValues(item);
                }
            }

            foreach (var item in pedidoExistente.PedidoItens)
            {
                if (!pedido.PedidoItens.Any(p => p.Id == item.Id))
                {
                    Db.Remove(item);
                }
            }
            #endregion Itens

            await SaveChanges();
        }

        public bool JaExiste(Guid id, int codigo)
        {
            return Buscar(f => f.Codigo == codigo && f.Id != id).Result.Any();
        }
        public bool Encontrou(Guid id)
        {
            return Buscar(f => f.Id == id).Result.Any();
        }
    }
}
