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
    [Collection(nameof(FormaPagamentoAutoMockerCollection))]
    public class FormaPagamentoServiceTests
    {
        readonly FormaPagamentoTestsAutoMockerFixture _formaPagamentoTestsAutoMockerFixture;

        private readonly FormaPagamentoService _formaPagamentoService;

        public FormaPagamentoServiceTests(FormaPagamentoTestsAutoMockerFixture formaPagamentoTestsFixture)
        {
            _formaPagamentoTestsAutoMockerFixture = formaPagamentoTestsFixture;
            _formaPagamentoService = _formaPagamentoTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _formaPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _formaPagamentoService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }        

        [Fact(DisplayName = "Adicionar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _formaPagamentoTestsAutoMockerFixture.GerarRegistroInvalido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _formaPagamentoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 2);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Never);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Never);            
        }
       

        [Fact(DisplayName = "Adicionar com Falha no registro Já Existe")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Adicionar_DeveFalharDevidoRegistroExistente()
        {
            // Arrange
            var registro = _formaPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(true);

            // Act
            var retorno = await _formaPagamentoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar
               
        #region Atualizar

        [Fact(DisplayName = "Atualizar Carro com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var arquivo = _formaPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(arquivo.Id, arquivo.Nome))
                .Returns(false);

            // Act
            var retorno = await _formaPagamentoService.Atualizar(arquivo);

            // Assert
            Assert.True(retorno);
            Assert.True(arquivo.EhValido());
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(arquivo.Id, arquivo.Nome), Times.Once);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Atualizar(arquivo), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _formaPagamentoTestsAutoMockerFixture.GerarRegistroInvalido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(false);

            // Act
            var retorno = await _formaPagamentoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 2);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Never);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Carro com Falha no Registro já Existe")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Atualizar_DeveFalharDevidoArquivoExistente()
        {
            // Arrange
            var registro = _formaPagamentoTestsAutoMockerFixture.GerarRegistroValido();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.JaExiste(registro.Id, registro.Nome))
                .Returns(true);

            // Act
            var retorno = await _formaPagamentoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.True(registro.EhValido());
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.JaExiste(registro.Id, registro.Nome), Times.Once);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar
               
        #region Remover       
        [Fact(DisplayName = "Remover com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.Encontrou(id))
                .Returns(true);

            // Act
            var retorno = await _formaPagamentoService.Remover(id);

            // Assert
            Assert.True(retorno);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }

        [Fact(DisplayName = "Remover com Falha já Existente")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.Encontrou(id))
                .Returns(false);

            // Act
            var retorno = await _formaPagamentoService.Remover(id);

            // Assert
            Assert.False(retorno);
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Encontrou(id), Times.Once);

        }
        #endregion Remover
       
        #region Obter Todos
        [Fact(DisplayName = "Obter Todos com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.ObterTodos(filtro))
                .ReturnsAsync(new ResponseModel<FormaPagamento>(_formaPagamentoTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _formaPagamentoService.ObterTodos(filtro);

            // Assert 
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter por Id com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_formaPagamentoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _formaPagamentoService.ObterPorId(id);

            // Assert 
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_formaPagamentoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _formaPagamentoService.Obter(id);

            // Assert 
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter

        #region Buscar
        [Fact(DisplayName = "Buscar com Sucesso")]
        [Trait("Categoria", "Forma de Pagamento Service Tests")]
        public async void FormaPagamentoService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<Models.FormaPagamento, bool>> predicate = (x) => x.Nome != string.Empty;
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_formaPagamentoTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _formaPagamentoService.Buscar(predicate);

            // Assert 
            _formaPagamentoTestsAutoMockerFixture.Mocker.GetMock<IFormaPagamentoRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar
    }
}
