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
    [Collection(nameof(UnidadeAutoMockerCollection))]
    public class UnidadeServiceTests
    {
        readonly UnidadeTestsAutoMockerFixture _unidadeTestsAutoMockerFixture;

        private readonly UnidadeService _unidadeService;

        public UnidadeServiceTests(UnidadeTestsAutoMockerFixture unidadeTestsFixture)
        {
            _unidadeTestsAutoMockerFixture = unidadeTestsFixture;
            _unidadeService = _unidadeTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _unidadeTestsAutoMockerFixture.GerarRegistroValido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.Sigla))
                .Returns(false);

            // Act
            var retorno = await _unidadeService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.Sigla), Times.Once);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }

        [Fact(DisplayName = "Adicionar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _unidadeTestsAutoMockerFixture.GerarRegistroInvalido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.Sigla))
                .Returns(false);

            // Act
            var retorno = await _unidadeService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 2);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.Sigla), Times.Never);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }


        [Fact(DisplayName = "Adicionar com Falha no registro Já Existe")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _unidadeTestsAutoMockerFixture.GerarRegistroValido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.Sigla))
                .Returns(true);

            // Act
            var retorno = await _unidadeService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.Sigla), Times.Once);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar

        #region Atualizar

        [Fact(DisplayName = "Atualizar Carro com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var arquivo = _unidadeTestsAutoMockerFixture.GerarRegistroValido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(arquivo.Id, arquivo.Sigla))
                .Returns(false);

            // Act
            var retorno = await _unidadeService.Atualizar(arquivo);

            // Assert
            Assert.True(retorno);
            Assert.True(arquivo.EhValido());
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(arquivo.Id, arquivo.Sigla), Times.Once);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Atualizar(arquivo), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _unidadeTestsAutoMockerFixture.GerarRegistroInvalido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.Sigla))
                .Returns(false);

            // Act
            var retorno = await _unidadeService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 2);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.Sigla), Times.Never);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha no Registro já Existe")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Atualizar_DeveFalharDevidoArquivoExistente()
        {
            // Arrange
            var registro = _unidadeTestsAutoMockerFixture.GerarRegistroValido();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.Sigla))
                .Returns(true);

            // Act
            var retorno = await _unidadeService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.Sigla), Times.Once);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar

        #region Remover       
        [Fact(DisplayName = "Remover com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _unidadeService.Remover(id);

            // Assert
            Assert.True(retorno);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }

        [Fact(DisplayName = "Remover com Falha já Existente")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _unidadeService.Remover(id);

            // Assert
            Assert.False(retorno);
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }
        #endregion Remover

        #region Obter Todos
        [Fact(DisplayName = "Obter Todos com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.ObterTodos(filtro))
                .ReturnsAsync(new ResponseModel<Unidade>(_unidadeTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _unidadeService.ObterTodos(filtro);

            // Assert 
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter por Id com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_unidadeTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _unidadeService.ObterPorId(id);

            // Assert 
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_unidadeTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _unidadeService.Obter(id);

            // Assert 
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter

        #region Buscar
        [Fact(DisplayName = "Buscar com Sucesso")]
        [Trait("Categoria", "Unidade Service Tests")]
        public async void UnidadeService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<Models.Unidade, bool>> predicate = (x) => x.Sigla != string.Empty;
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_unidadeTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _unidadeService.Buscar(predicate);

            // Assert 
            _unidadeTestsAutoMockerFixture.Mocker.GetMock<IUnidadeRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar
    }
}
