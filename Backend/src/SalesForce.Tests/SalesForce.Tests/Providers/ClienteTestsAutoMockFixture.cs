using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using ERP.Business.Services;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ERP.Business.Tests.Providers
{
    [CollectionDefinition(nameof(ClienteAutoMockerCollection))]
    public class ClienteAutoMockerCollection : ICollectionFixture<ClienteTestsAutoMockerFixture>
    {
    }

    public class ClienteTestsAutoMockerFixture : IDisposable
    {
        public ClienteService ClienteService;
        public AutoMocker Mocker;

        public ERP.Business.Models.Cliente GerarClienteValido()
        {
            return GerarClientes(1, true).FirstOrDefault();
        }

        public IEnumerable<ERP.Business.Models.Cliente> ObterClientesVariados()
        {
            var clientes = new List<ERP.Business.Models.Cliente>();

            clientes.AddRange(GerarClientes(50, true).ToList());
            clientes.AddRange(GerarClientes(50, false).ToList());

            return clientes;
        }

        public IEnumerable<ERP.Business.Models.Cliente> GerarClientes(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var clientes = new Faker<ERP.Business.Models.Cliente>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.Cliente
                {
                    Nome = f.Name.FullName(genero),
                    CnpjCpfDi = f.Company.Cnpj(),
                    Endereco = f.Address.StreetName(),
                    Numero = f.Random.Number(90000).ToString(),
                    Bairro = f.Name.FullName(genero),
                    Cep = f.Address.ZipCode(),
                    CidadeId = Guid.NewGuid(),
                    Ativo = true,
                    TipoPessoa = "F",
                    Telefone = f.Phone.PhoneNumber(),
                    Complemento = f.Address.SecondaryAddress(),
                    Email = f.Internet.Email(),
                    InscricaoEstadual = f.Company.Cnpj(),
                    TipoInscricaoEstadual = 9
                })
                .RuleFor(c => c.Email, (f, c) =>
                      f.Internet.Email(c.Nome.ToLower()));

            return clientes.Generate(quantidade);
        }

        public ERP.Business.Models.Cliente GerarClienteInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var cliente = new Faker<ERP.Business.Models.Cliente>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.Cliente
                {
                    Nome = string.Empty,
                    CnpjCpfDi = string.Empty,
                    Endereco = string.Empty,
                    Numero = string.Empty,
                    Bairro = string.Empty,
                    Cep = string.Empty,
                    CidadeId = Guid.NewGuid(),
                    Ativo = true,
                    TipoPessoa = "F",
                    Telefone = string.Empty,
                    Complemento = string.Empty,
                    Email = string.Empty,
                    InscricaoEstadual = string.Empty,
                    TipoInscricaoEstadual = 9
                });

            return cliente;
        }

        public ClienteService ObterClienteService()
        {
            Mocker = new AutoMocker();
            ClienteService = Mocker.CreateInstance<ClienteService>();

            return ClienteService;
        }
        public void Dispose()
        {
        }
    }
}
