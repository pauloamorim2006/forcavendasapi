using Bogus;
using Bogus.DataSets;
using ERP.Business.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ERP.Business.Tests.Providers
{
    [CollectionDefinition(nameof(FormaPagamentoAutoMockerCollection))]
    public class FormaPagamentoAutoMockerCollection : ICollectionFixture<FormaPagamentoTestsAutoMockerFixture>
    {
    }

    public class FormaPagamentoTestsAutoMockerFixture : IDisposable
    {
        public FormaPagamentoService FormaPagamentoService;
        public AutoMocker Mocker;

        public Models.FormaPagamento GerarRegistroValido()
        {
            return GerarList(1, true).FirstOrDefault();
        }

        public IEnumerable<ERP.Business.Models.FormaPagamento> ObterVariados()
        {
            var list = new List<ERP.Business.Models.FormaPagamento>();

            list.AddRange(GerarList(50, true).ToList());
            list.AddRange(GerarList(50, false).ToList());

            return list;
        }

        public IEnumerable<Models.FormaPagamento> GerarList(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var list = new Faker<Models.FormaPagamento>("pt_BR")
                .CustomInstantiator(f => new Models.FormaPagamento
                {
                    Nome = f.Name.FullName(genero),
                    Ativo = true,
                    Tipo = "D",
                    Tef = true,
                    Credito = true,
                    PermitirTroco = true,
                    ConfiguracaoFiscal = "01"
                 });

            return list.Generate(quantidade);
        }

        public Models.FormaPagamento GerarRegistroInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var objeto = new Faker<Models.FormaPagamento>("pt_BR")
                .CustomInstantiator(f => new Models.FormaPagamento
                {
                });

            return objeto;
        }

        public FormaPagamentoService ObterService()
        {
            Mocker = new AutoMocker();
            FormaPagamentoService = Mocker.CreateInstance<FormaPagamentoService>();

            return FormaPagamentoService;
        }

        public void Dispose()
        {
        }
    }
}
