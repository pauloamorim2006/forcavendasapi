using Bogus;
using Bogus.DataSets;
using ERP.Business.Models;
using ERP.Business.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ERP.Business.Tests.Providers
{
    [CollectionDefinition(nameof(PedidoAutoMockerCollection))]
    public class PedidoAutoMockerCollection : ICollectionFixture<PedidoTestsAutoMockerFixture>
    {
    }

    public class PedidoTestsAutoMockerFixture : IDisposable
    {
        public PedidoService PedidoService;
        public AutoMocker Mocker;

        public Models.Pedido GerarRegistroValido()
        {
            return GerarList(1, true).FirstOrDefault();
        }

        public IEnumerable<ERP.Business.Models.Pedido> ObterVariados()
        {
            var list = new List<ERP.Business.Models.Pedido>();

            list.AddRange(GerarList(50, true).ToList());
            list.AddRange(GerarList(50, false).ToList());

            return list;
        }

        public IEnumerable<Models.Pedido> GerarList(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var list = new Faker<Models.Pedido>("pt_BR")
                .CustomInstantiator(f => new Models.Pedido
                {
                    Codigo = f.Random.Number(),
                    Status = StatusPedido.Aberto,
                    ClienteId = Guid.NewGuid(),
                    Data = DateTime.Now,
                    CondicaoPagamentoId = Guid.NewGuid(),
                    FormaPagamentoId = Guid.NewGuid(),
                    PedidoItens = new List<PedidoItem>
                        {
                            new PedidoItem
                            {
                                PedidoId = Guid.NewGuid(),
                                Item = f.Random.Number(),
                                ProdutoId = Guid.NewGuid(),
                                Quantidade = f.Random.Number(1, 10000),
                                ValorUnitario = f.Random.Number(1, 10000),
                                ValorDesconto = f.Random.Number(1, 10000),
                                ValorAcrescimo = f.Random.Number(1, 10000),
                                ValorTotal = 10
                             }
                        }
                });

            return list.Generate(quantidade);
        }

        public Models.Pedido GerarRegistroInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var objeto = new Faker<Models.Pedido>("pt_BR")
                .CustomInstantiator(f => new Models.Pedido
                {
                });

            return objeto;
        }

        public PedidoService ObterService()
        {
            Mocker = new AutoMocker();
            PedidoService = Mocker.CreateInstance<PedidoService>();

            return PedidoService;
        }
        
        public void Dispose()
        {
        }
    }
}
