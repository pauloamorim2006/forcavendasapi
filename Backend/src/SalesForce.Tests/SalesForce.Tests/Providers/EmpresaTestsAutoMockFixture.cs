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
    [CollectionDefinition(nameof(EmpresaAutoMockerCollection))]
    public class EmpresaAutoMockerCollection : ICollectionFixture<EmpresaTestsAutoMockerFixture>
    {
    }

    public class EmpresaTestsAutoMockerFixture : IDisposable
    {
        public EmpresaService EmpresaService;
        public AutoMocker Mocker;

        public ERP.Business.Models.Empresa GerarRegistroValido()
        {
            return GerarList(1, true).FirstOrDefault();
        }

        public IEnumerable<ERP.Business.Models.Empresa> ObterVariados()
        {
            var list = new List<ERP.Business.Models.Empresa>();

            list.AddRange(GerarList(50, true).ToList());
            list.AddRange(GerarList(50, false).ToList());

            return list;
        }

        public IEnumerable<ERP.Business.Models.Empresa> GerarList(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var list = new Faker<ERP.Business.Models.Empresa>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.Empresa
                {
                    Nome = f.Name.FullName(genero),
                    Fantasia = f.Name.FullName(genero),
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
                    TipoInscricaoEstadual = 9,
                    Padrao = true,
                    Crt = 1
                });

            return list.Generate(quantidade);
        }

        public ERP.Business.Models.Empresa GerarRegistroInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var objeto = new Faker<ERP.Business.Models.Empresa>("pt_BR")
                .CustomInstantiator(f => new ERP.Business.Models.Empresa());

            return objeto;
        }

        public EmpresaService ObterService()
        {
            Mocker = new AutoMocker();
            EmpresaService = Mocker.CreateInstance<EmpresaService>();

            return EmpresaService;
        }
        public void Dispose()
        {
        }
    }
}
