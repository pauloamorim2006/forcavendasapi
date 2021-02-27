using Bogus;
using Bogus.DataSets;
using ERP.Business.Intefaces;
using ERP.Business.Services;
using ERP.Data.Repository;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ERP.Business.Tests.Providers
{
    [CollectionDefinition(nameof(CondicaoPagamentoAutoMockerCollection))]
    public class CondicaoPagamentoAutoMockerCollection : ICollectionFixture<CondicaoPagamentoTestsAutoMockerFixture>
    {
    }

    public class CondicaoPagamentoTestsAutoMockerFixture : IDisposable
    {
        public CondicaoPagamentoService CondicaoPagamentoService;
        public CondicaoPagamentoRepository CondicaoPagamentoRepository;
        public AutoMocker Mocker;

        public ERP.Business.Models.CondicaoPagamento GerarRegistroValido()
        {
            return GerarList(1, true).FirstOrDefault();
        }

        public IEnumerable<ERP.Business.Models.CondicaoPagamento> ObterVariados()
        {
            var list = new List<ERP.Business.Models.CondicaoPagamento>();

            list.AddRange(GerarList(50, true).ToList());
            list.AddRange(GerarList(50, false).ToList());

            return list;
        }

        public IEnumerable<ERP.Business.Models.CondicaoPagamento> GerarList(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var list = new Faker<ERP.Business.Models.CondicaoPagamento>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.CondicaoPagamento
                {
                    Nome = f.Name.FullName(genero),
                    Descricao = f.Name.FullName(genero)
                });

            return list.Generate(quantidade);
        }

        public ERP.Business.Models.CondicaoPagamento GerarRegistroInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var objeto = new Faker<ERP.Business.Models.CondicaoPagamento>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.CondicaoPagamento
                {
                    Nome = string.Empty,
                    Descricao = string.Empty
                });

            return objeto;
        }

        public CondicaoPagamentoService ObterService()
        {
            Mocker = new AutoMocker();
            CondicaoPagamentoService = Mocker.CreateInstance<CondicaoPagamentoService>();

            return CondicaoPagamentoService;
        }

        public ICondicaoPagamentoRepository ObterRepository()
        {
            Mocker = new AutoMocker();
            CondicaoPagamentoRepository = Mocker.CreateInstance<CondicaoPagamentoRepository>();

            return CondicaoPagamentoRepository;
        }

        public void Dispose()
        {
        }
    }
}
