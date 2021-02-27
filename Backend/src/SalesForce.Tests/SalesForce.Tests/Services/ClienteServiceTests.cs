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
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceTests
    {
        readonly ClienteTestsAutoMockerFixture _clienteTestsAutoMockerFixture;

        private readonly ClienteService _clienteService;

        public ClienteServiceTests(ClienteTestsAutoMockerFixture clienteTestsFixture)
        {
            _clienteTestsAutoMockerFixture = clienteTestsFixture;
            _clienteService = _clienteTestsAutoMockerFixture.ObterClienteService();
        }
        
        #region Adicionar

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _clienteService.Adicionar(cliente);

            // Assert
            Assert.True(retorno);
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);            
        }
        
        [Fact(DisplayName = "Adicionar Cliente com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _clienteService.Adicionar(cliente);

            // Assert
            Assert.False(retorno);
            Assert.False(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);            
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha no Cliente já Existe")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Adicionar_DeveFalharDevidoClienteExistente()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(true);

            // Act
            var retorno = await _clienteService.Adicionar(cliente);

            // Assert
            Assert.False(retorno);
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
        }
        
        #endregion Adicionar

        #region Atualizar

        [Fact(DisplayName = "Atualizar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _clienteService.Atualizar(cliente);

            // Assert
            Assert.True(retorno);
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi), Times.Once);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Atualizar(cliente), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Cliente com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Atualizar_DeveFalharDevidoClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteInvalido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(false);

            // Act
            var retorno = await _clienteService.Atualizar(cliente);

            // Assert
            Assert.False(retorno);
            Assert.False(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Atualizar(cliente), Times.Never);
        }

        [Fact(DisplayName = "Atualizar Cliente com Falha no Cliente já Existe")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Atualizar_DeveFalharDevidoClienteExistente()
        {
            // Arrange
            var cliente = _clienteTestsAutoMockerFixture.GerarClienteValido();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
                .Returns(true);

            // Act
            var retorno = await _clienteService.Atualizar(cliente);

            // Assert
            Assert.False(retorno);
            Assert.True(cliente.EhValido());
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Atualizar(cliente), Times.Never);
        }

        #endregion Atualizar

        #region Remover       
        [Fact(DisplayName = "Remover Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.EncontrouCliente(id))
                .Returns(true);

            // Act
            var retorno = await _clienteService.Remover(id);

            // Assert
            Assert.True(retorno);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.EncontrouCliente(id), Times.Once);

        }

        [Fact(DisplayName = "Remover Cliente com Falha já Existente")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            var id = Guid.NewGuid();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.EncontrouCliente(id))
                .Returns(false);

            // Act
            var retorno = await _clienteService.Remover(id);

            // Assert
            Assert.False(retorno);
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.EncontrouCliente(id), Times.Once);

        }
        #endregion Remover

        #region Recuperar Todos
        [Fact(DisplayName = "Recuperar Todos os Clientes com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_RecuperarTodos_DeveExecutarComSucesso()
        {
            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.RecuperarTodos())
                .Returns(Task.FromResult(_clienteTestsAutoMockerFixture.ObterClientesVariados().ToList()));

            // Act
            var clientes = await _clienteService.RecuperarTodos();

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.RecuperarTodos(), Times.Once);
            Assert.True(clientes.Any());
        }
        #endregion Recuperar Todos

        #region Obter Todos
        [Fact(DisplayName = "Obter Todos os Clientes com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
                .Returns(Task.FromResult(_clienteTestsAutoMockerFixture.ObterClientesVariados().ToList()));

            // Act
            var clientes = await _clienteService.ObterTodos();

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter Cliente por Id com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_clienteTestsAutoMockerFixture.GerarClienteValido()));

            // Act
            var cliente = await _clienteService.ObterPorId(id);

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(cliente != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_clienteTestsAutoMockerFixture.GerarClienteValido()));

            // Act
            var cliente = await _clienteService.Obter(id);

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(cliente != null);
        }
        #endregion Obter Por Id

        #region Buscar
        [Fact(DisplayName = "Buscar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<ERP.Business.Models.Cliente, bool>> predicate = (x) => x.Ativo == true;
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_clienteTestsAutoMockerFixture.ObterClientesVariados()));

            // Act
            var cliente = await _clienteService.Buscar(predicate);

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(cliente != null);
        }
        #endregion Buscar

        #region Recuperar Quantidade
        [Fact(DisplayName = "Recuperar Quantidade de Clientes com Sucesso")]
        [Trait("Categoria", "Cliente Service Tests")]
        public async void ClienteService_RecuperarQuantidade_DeveExecutarComSucesso()
        {
            // Arrange
            var quantidade = _clienteTestsAutoMockerFixture.ObterClientesVariados().Count();
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.RecuperarQuantidade())
                .Returns(Task.FromResult(quantidade));

            // Act
            var quantidadeRetorno = await _clienteService.RecuperarQuantidade();

            // Assert 
            _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.RecuperarQuantidade(), Times.Once);
            Assert.True(quantidade == quantidadeRetorno);
        }
        #endregion Recuperar Quantidade
    }
}
