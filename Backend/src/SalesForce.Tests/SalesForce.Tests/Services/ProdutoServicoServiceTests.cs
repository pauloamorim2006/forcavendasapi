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
    [Collection(nameof(ProdutoServicoAutoMockerCollection))]
    public class ProdutoServicoServiceTests
    {
        readonly ProdutoServicoTestsAutoMockerFixture _produtoServicoTestsAutoMockerFixture;

        private readonly ProdutoServicoService _produtoServicoService;

        public ProdutoServicoServiceTests(ProdutoServicoTestsAutoMockerFixture produtoServicoTestsFixture)
        {
            _produtoServicoTestsAutoMockerFixture = produtoServicoTestsFixture;
            _produtoServicoService = _produtoServicoTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _produtoServicoTestsAutoMockerFixture.GerarRegistroValido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _produtoServicoService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }

        [Fact(DisplayName = "Adicionar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _produtoServicoTestsAutoMockerFixture.GerarRegistroInvalido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _produtoServicoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 1);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Never);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }


        [Fact(DisplayName = "Adicionar com Falha no registro Já Existe")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _produtoServicoTestsAutoMockerFixture.GerarRegistroValido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(true);

            // Act
            var retorno = await _produtoServicoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar
                         
        #region Atualizar

        [Fact(DisplayName = "Atualizar Carro com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var arquivo = _produtoServicoTestsAutoMockerFixture.GerarRegistroValido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(arquivo.Id, arquivo.Nome))
                .Returns(false);

            // Act
            var retorno = await _produtoServicoService.Atualizar(arquivo);

            // Assert
            Assert.True(retorno);
            Assert.True(arquivo.EhValido());
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(arquivo.Id, arquivo.Nome), Times.Once);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Atualizar(arquivo), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _produtoServicoTestsAutoMockerFixture.GerarRegistroInvalido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _produtoServicoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 1);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Never);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha no Registro já Existe")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Atualizar_DeveFalharDevidoArquivoExistente()
        {
            // Arrange
            var registro = _produtoServicoTestsAutoMockerFixture.GerarRegistroValido();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(true);

            // Act
            var retorno = await _produtoServicoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar       

        #region Remover       
        [Fact(DisplayName = "Remover com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _produtoServicoService.Remover(id);

            // Assert
            Assert.True(retorno);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }

        [Fact(DisplayName = "Remover com Falha já Existente")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _produtoServicoService.Remover(id);

            // Assert
            Assert.False(retorno);
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }
        #endregion Remover        

        #region Obter Todos
        [Fact(DisplayName = "Obter Todos com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.ObterTodos(filtro))
                .ReturnsAsync(new ResponseModel<ProdutoServico>(_produtoServicoTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _produtoServicoService.ObterTodos(filtro);

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos        

        #region Obter Por Id
        [Fact(DisplayName = "Obter por Id com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_produtoServicoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _produtoServicoService.ObterPorId(id);

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id        

        #region Obter
        [Fact(DisplayName = "Obter com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_produtoServicoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _produtoServicoService.Obter(id);

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter       

        #region Buscar
        [Fact(DisplayName = "Buscar com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<Models.ProdutoServico, bool>> predicate = (x) => x.Nome != string.Empty;
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_produtoServicoTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _produtoServicoService.Buscar(predicate);

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar

        #region Recuperar Todos
        [Fact(DisplayName = "Recuperar Todos com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_RecuperarTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _ = _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.RecuperarTodos(filtro))
                .ReturnsAsync(new ResponseModel<ProdutoServico>(_produtoServicoTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _produtoServicoService.RecuperarTodos(filtro);

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.RecuperarTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Recuperar Todos  

        #region Recuperar Quantidade
        [Fact(DisplayName = "Recuperar Quantidade com Sucesso")]
        [Trait("Categoria", "Produto Servico Service Tests")]
        public async void ProdutoServicoService_RecuperarQuantidade_DeveExecutarComSucesso()
        {
            // Arrange
            var quantidade = _produtoServicoTestsAutoMockerFixture.ObterVariados().Count();
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Setup(c => c.RecuperarQuantidade())
                .Returns(Task.FromResult(quantidade));

            // Act
            var quantidadeRetorno = await _produtoServicoService.RecuperarQuantidade();

            // Assert 
            _produtoServicoTestsAutoMockerFixture.Mocker.GetMock<IProdutoServicoRepository>().Verify(r => r.RecuperarQuantidade(), Times.Once);
            Assert.True(quantidade == quantidadeRetorno);
        }
        #endregion Recuperar Quantidade
    }
}
