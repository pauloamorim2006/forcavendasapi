using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Business.Models.Validations;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Services
{
    public class PedidoService : BaseService, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;        

        public PedidoService(IPedidoRepository pedidoRepository,                             
                             INotificador notificador) : base(notificador)
        {
            _pedidoRepository = pedidoRepository;            
        }

        public async Task<bool> Adicionar(Pedido pedido)
        {
            if (!ExecutarValidacao(new PedidoValidation(), pedido)) return false;
            
           await _pedidoRepository.Adicionar(pedido);
            return true;
        }

        public async Task<List<double>> RecuperarPorMes()
        {
            try
            {
                var mesAtual = new List<double>();

                var dataInicialFiltro = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1,
                    0,
                    0,
                    0);
                var dataCorrente = dataInicialFiltro;

                // 12 meses do ano atual
                for (int i = 1; i <= 12; i++)
                {
                    DateTime dataInicial =
                        new DateTime(
                        dataCorrente.Year,
                        i,
                        1,
                        0,
                        0,
                        0);
                    DateTime dataFinal =
                        new DateTime(
                        dataInicial.Year,
                        dataInicial.Month,
                        DateTime.DaysInMonth(dataInicial.Year, dataInicial.Month),
                        23,
                        59,
                        59);
                    var lista = await
                        _pedidoRepository.RecuperarTodos(dataInicial, dataFinal, Guid.Empty, (int)StatusPedido.Faturado);
                    var item = lista.ConvertAll(x =>
                    {
                        return new { ValorTotal = x.PedidoItens.Sum(n => n.ValorTotal) };
                    }).Sum(t => t.ValorTotal);

                    mesAtual.Add(item);

                    dataCorrente = dataCorrente.AddDays(1);
                }

                return mesAtual;
            }
            catch (Exception error)
            {
                Notificar($"Não foi possível encontrar os pedidos. Motivo: {error.Message}");
                return null;                
            }
        }

        public async Task<bool> Atualizar(Pedido pedido)
        {
            var registro = await _pedidoRepository.Obter(pedido.Id);

            if (!Validar(registro)) return false;

            if (!ExecutarValidacao(new PedidoValidation(), pedido)) return false;
            
            pedido.Codigo = registro.Codigo;

            await _pedidoRepository.Atualizar(pedido);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            var registro = await _pedidoRepository.RecuperarPorId(id);

            if (!Validar(registro)) return false;

            await _pedidoRepository.Remover(id);
            return true;
        }        

        public async Task<bool> Cancelar(Guid id)
        {
            var registro = await _pedidoRepository.Obter(id);

            if (!Validar(registro)) return false;

            try
            {
                registro.Status = StatusPedido.Cancelado;
                await _pedidoRepository.Atualizar(registro);
                return true;
            }
            catch (Exception error)
            {
                Notificar($"Não foi possível cancelar o pedido. Motivo: {error.Message}");
                return false;
            }
        }

        private bool Validar(Pedido registro)
        {
            if (registro == null)
            {
                Notificar($"Pedido não encontrado!");
                return false;
            }

            if (registro.Status != StatusPedido.Aberto)
            {
                Notificar($"Pedido não deverá ser diferente de 'Aberto'!");
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Pedido>> Buscar(Expression<Func<Pedido, bool>> predicate)
        {
            return await _pedidoRepository.Buscar(predicate);
        }
        
        public async Task<Pedido> ObterPorId(Guid id)
        {
            return await _pedidoRepository.ObterPorId(id);
        }
        
        public async Task<Pedido> Obter(Guid id)
        {
            return await _pedidoRepository.Obter(id);
        }
        
        public async Task<ResponseModel<Pedido>> ObterTodos(PaginationFilter filter)
        {
            return await _pedidoRepository.ObterTodos(filter);
        }

        public async Task<int> RecuperarQuantidade()
        {
            return await _pedidoRepository.RecuperarQuantidade();
        }

        public async Task<List<Pedido>> RecuperarTodos(
            DateTime? DataInicial,
            DateTime? DataFinal,
            Guid? ClienteId,
            int? Status)
        {
            return await _pedidoRepository.RecuperarTodos(
                DataInicial,
                DataFinal,
                ClienteId,
                Status);
        }

        public async Task<Pedido> RecuperarPorId(Guid id)
        {
            return await _pedidoRepository.RecuperarPorId(id);
        }

        public void Dispose()
        {
            _pedidoRepository?.Dispose();
        }
    }
}
