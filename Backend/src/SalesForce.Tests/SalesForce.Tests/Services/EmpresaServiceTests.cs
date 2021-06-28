using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Business.Services;
using ERP.Business.Tests.Providers;
using Moq;
using SalesForce.Business.Responses;
using SalesForce.Business.Tests.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ERP.Business.Tests.Services
{
    [Collection(nameof(EmpresaAutoMockerCollection))]
    public class EmpresaServiceTests
    {
        readonly EmpresaTestsAutoMockerFixture _empresaTestsAutoMockerFixture;

        private readonly EmpresaService _empresaService;

        public EmpresaServiceTests(EmpresaTestsAutoMockerFixture empresaTestsFixture)
        {
            _empresaTestsAutoMockerFixture = empresaTestsFixture;
            _empresaService = _empresaTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroValido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _empresaService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Once);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }
        
        [Fact(DisplayName = "Adicionar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroInvalido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _empresaService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 9);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Adicionar(registro), Times.Never); Assert.False(registro.EhValido());
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Never);                        
        }

        [Fact(DisplayName = "Adicionar registro Já Existe")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroValido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(true);

            // Act
            var retorno = await _empresaService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Adicionar(registro), Times.Never);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Once);
        }

        #endregion Adicionar
        
        #region Atualizar

        [Fact(DisplayName = "Atualizar com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroValido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _empresaService.Atualizar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Once);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Atualizar(registro), Times.Once);
        }
        
        [Fact(DisplayName = "Atualizar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroInvalido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _empresaService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 9);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Atualizar(registro), Times.Never);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Never);
        }

        [Fact(DisplayName = "Atualizar com Falha no Registro já Existe")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Atualizar_DeveFalharDevidoArquivoExistente()
        {
            // Arrange
            var registro = _empresaTestsAutoMockerFixture.GerarRegistroValido();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.JaExiste(registro.Id, registro.CnpjCpfDi))
                .Returns(true);

            // Act
            var retorno = await _empresaService.Atualizar(registro);

            // Assert
            Assert.False(retorno);            
            Assert.True(registro.EhValido());
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.JaExiste(registro.Id, registro.CnpjCpfDi), Times.Once);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Atualizar(registro), Times.Never);
            
        }

        #endregion Atualizar
        
        #region Remover       
        [Fact(DisplayName = "Remover Banco com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _empresaService.Remover(id);

            // Assert
            Assert.True(retorno);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Encontrou(id), Times.Once);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Remover(id), Times.Once);

        }

        [Fact(DisplayName = "Remover com Falha já Existente")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _empresaService.Remover(id);

            // Assert
            Assert.False(retorno);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Encontrou(id), Times.Once);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Remover(id), Times.Never);

        }
        #endregion Remover
        
        #region Obter Todos
        [Fact(DisplayName = "Obter Todos com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.ObterTodos(filtro))
                .ReturnsAsync(new ResponseModel<Empresa>(_empresaTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _empresaService.ObterTodos(filtro);

            // Assert 
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos
       
        #region Obter Por Id
        [Fact(DisplayName = "Obter por Id com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_empresaTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _empresaService.ObterPorId(id);

            // Assert 
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id        

        #region Obter
        [Fact(DisplayName = "Obter com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_empresaTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _empresaService.Obter(id);

            // Assert 
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter        

        #region Buscar
        [Fact(DisplayName = "Buscar com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<ERP.Business.Models.Empresa, bool>> predicate = (x) => x.CnpjCpfDi != string.Empty;
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_empresaTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _empresaService.Buscar(predicate);

            // Assert 
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar

        #region Buscar Pelo Contexto
        [Fact(DisplayName = "Buscar Pelo Contexto com Sucesso")]
        [Trait("Categoria", "Empresa Service Tests")]
        public async void EmpresaService_BuscarPeloContexto_DeveExecutarComSucesso()
        {
            // Arrange
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Setup(c => c.Buscar())
                .Returns(Task.FromResult(_empresaTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _empresaService.Buscar();

            // Assert 
            Assert.True(registro != null);
            _empresaTestsAutoMockerFixture.Mocker.GetMock<IEmpresaRepository>().Verify(r => r.Buscar(), Times.Once);            
        }
        #endregion Buscar Pelo Acesso

    }
}
