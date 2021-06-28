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
    [Collection(nameof(CondicaoPagamentoAutoMockerCollection))]
    public class CondicaoPagamentoServiceTests
    {
        readonly CondicaoPagamentoTestsAutoMockerFixture _condicaoPagamentoTestsAutoMockerFixture;

        private readonly CondicaoPagamentoService _condicaoPagamentoService;

        public CondicaoPagamentoServiceTests(CondicaoPagamentoTestsAutoMockerFixture condicaoPagamentoTestsFixture)
        {
            _condicaoPagamentoTestsAutoMockerFixture = condicaoPagamentoTestsFixture;
            _condicaoPagamentoService = _condicaoPagamentoTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar Condição de Pagamento com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(false);

            // Act
            var retorno = await _condicaoPagamentoService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Descricao), Times.Once);
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Condição de Pagamento com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroInvalido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(false);

            // Act
            var retorno = await _condicaoPagamentoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Condição de Pagamento com Falha no registro Já Existe")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(true);

            // Act
            var retorno = await _condicaoPagamentoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar

        #region Atualizar

        [Fact(DisplayName = "Atualizar Condição de Pagamento com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(false);

            // Act
            var retorno = await _condicaoPagamentoService.Atualizar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Descricao), Times.Once);
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Atualizar(registro), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Condição de Pagamento com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroInvalido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(false);

            // Act
            var retorno = await _condicaoPagamentoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Condição de Pagamento com Falha no Registro já Existe")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Atualizar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Descricao))
                .Returns(true);

            // Act
            var retorno = await _condicaoPagamentoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar

        #region Remover       
        [Fact(DisplayName = "Remover Condição de Pagamento com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _condicaoPagamentoService.Remover(id);

            // Assert
            Assert.True(retorno);
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }

        [Fact(DisplayName = "Remover Condição de Pagamento com Falha já Existente")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _condicaoPagamentoService.Remover(id);

            // Assert
            Assert.False(retorno);
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }
        #endregion Remover

        #region Obter Todos
        [Fact(DisplayName = "Obter Todas as Condições com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.ObterTodos(filtro))
                .Returns(Task.FromResult(new ResponseModel<CondicaoPagamento>(_condicaoPagamentoTestsAutoMockerFixture.ObterVariados().ToList(), 1000)));

            // Act
            var list = await _condicaoPagamentoService.ObterTodos(filtro);

            // Assert 
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter Condição de Pagamento por Id com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _condicaoPagamentoService.ObterPorId(id);

            // Assert 
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter Condição de Pagamento com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_condicaoPagamentoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _condicaoPagamentoService.Obter(id);

            // Assert 
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter

        #region Buscar
        [Fact(DisplayName = "Buscar Condição de Pagamento com Sucesso")]
        [Trait("Categoria", "Condição de Pagamento Service Tests")]
        public async void CondicaoPagamentoService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<ERP.Business.Models.CondicaoPagamento, bool>> predicate = (x) => x.Descricao != string.Empty;
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_condicaoPagamentoTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _condicaoPagamentoService.Buscar(predicate);

            // Assert 
            _condicaoPagamentoTestsAutoMockerFixture.Mocker.GetMock<ICondicaoPagamentoRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar

    }
}
