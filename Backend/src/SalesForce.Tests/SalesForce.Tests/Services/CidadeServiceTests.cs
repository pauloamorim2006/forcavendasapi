using ERP.Business.Intefaces;
using ERP.Business.Services;
using ERP.Business.Tests.Providers;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ERP.Business.Tests.Services
{
    [Collection(nameof(CidadeAutoMockerCollection))]
    public class CidadeServiceTests
    {
        readonly CidadeTestsAutoMockerFixture _cidadeTestsAutoMockerFixture;

        private readonly CidadeService _cidadeService;

        public CidadeServiceTests(CidadeTestsAutoMockerFixture cidadeTestsFixture)
        {
            _cidadeTestsAutoMockerFixture = cidadeTestsFixture;
            _cidadeService = _cidadeTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar Cidade com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _cidadeTestsAutoMockerFixture.GerarRegistroValido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.CodigoIbge))
                .Returns(false);

            // Act
            var retorno = await _cidadeService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.JaExiste(registro.Id, registro.CodigoIbge), Times.Once);
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cidade com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _cidadeTestsAutoMockerFixture.GerarRegistroInvalido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.CodigoIbge))
                .Returns(false);

            // Act
            var retorno = await _cidadeService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        [Fact(DisplayName = "Adicionar Cidade com Falha no registro Já Existe")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _cidadeTestsAutoMockerFixture.GerarRegistroValido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.CodigoIbge))
                .Returns(true);

            // Act
            var retorno = await _cidadeService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar

        #region Atualizar

        [Fact(DisplayName = "Atualizar Cidade com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var arquivo = _cidadeTestsAutoMockerFixture.GerarRegistroValido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(arquivo.Id, arquivo.CodigoIbge))
                .Returns(false);

            // Act
            var retorno = await _cidadeService.Atualizar(arquivo);

            // Assert
            Assert.True(retorno);
            Assert.True(arquivo.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.JaExiste(arquivo.Id, arquivo.CodigoIbge), Times.Once);
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Atualizar(arquivo), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Cidade com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _cidadeTestsAutoMockerFixture.GerarRegistroInvalido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.CodigoIbge))
                .Returns(false);

            // Act
            var retorno = await _cidadeService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Cidade com Falha no Registro já Existe")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Atualizar_DeveFalharDevidoArquivoExistente()
        {
            // Arrange
            var registro = _cidadeTestsAutoMockerFixture.GerarRegistroValido();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.JaExiste(registro.Id, registro.CodigoIbge))
                .Returns(true);

            // Act
            var retorno = await _cidadeService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar

        #region Remover       
        [Fact(DisplayName = "Remover Cidade com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _cidadeService.Remover(id);

            // Assert
            Assert.True(retorno);
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }

        [Fact(DisplayName = "Remover Cidade com Falha já Existente")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _cidadeService.Remover(id);

            // Assert
            Assert.False(retorno);
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }
        #endregion Remover

        #region Obter Todos
        [Fact(DisplayName = "Obter Todos os Cidades com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.ObterTodos())
                .Returns(Task.FromResult(_cidadeTestsAutoMockerFixture.ObterVariados().ToList()));

            // Act
            var list = await _cidadeService.ObterTodos();

            // Assert 
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(list.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter Cidade por Id com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_cidadeTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _cidadeService.ObterPorId(id);

            // Assert 
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter Cidade com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_cidadeTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _cidadeService.Obter(id);

            // Assert 
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter

        #region Buscar
        [Fact(DisplayName = "Buscar Cidade com Sucesso")]
        [Trait("Categoria", "Cidade Service Tests")]
        public async void CidadeService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<ERP.Business.Models.Cidade, bool>> predicate = (x) => x.Descricao != string.Empty;
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_cidadeTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _cidadeService.Buscar(predicate);

            // Assert 
            _cidadeTestsAutoMockerFixture.Mocker.GetMock<ICidadeRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar

    }
}
