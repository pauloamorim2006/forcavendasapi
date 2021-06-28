using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Business.Services;
using ERP.Business.Tests.Providers;
using Moq;
using SalesForce.Business.Responses;
using SalesForce.Business.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ERP.Business.Tests.Services
{
    [Collection(nameof(PedidoAutoMockerCollection))]
    public class PedidoServiceTests
    {
        readonly PedidoTestsAutoMockerFixture _pedidoTestsAutoMockerFixture;

        private readonly PedidoService _pedidoService;

        public PedidoServiceTests(PedidoTestsAutoMockerFixture pedidoTestsFixture)
        {
            _pedidoTestsAutoMockerFixture = pedidoTestsFixture;
            _pedidoService = _pedidoTestsAutoMockerFixture.ObterService();
        }

        #region Adicionar

        [Fact(DisplayName = "Adicionar com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange                        
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroValido();                       

            // Act
            var retorno = await _pedidoService.Adicionar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(registro), Times.Once);
        }
       
        [Fact(DisplayName = "Adicionar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Adicionar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();

            // Act
            var retorno = await _pedidoService.Adicionar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 3);           
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(registro), Times.Never);
        }

        #endregion Adicionar

        #region Atualizar

        [Fact(DisplayName = "Atualizar com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroValido();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(registro.Id))
                .Returns(Task.FromResult(registro));

            // Act
            var retorno = await _pedidoService.Atualizar(registro);

            // Assert
            Assert.True(retorno);
            Assert.True(registro.EhValido());
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(registro.Id), Times.Once);            
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(registro), Times.Once);
        }

        
        [Fact(DisplayName = "Atualizar com Falha nos Dados da Entidade")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Atualizar_DeveFalharDevidoRegistroInvalido()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(registro.Id))
                .Returns(Task.FromResult(registro));

            // Act
            var retorno = await _pedidoService.Atualizar(registro);

            // Assert
            Assert.False(retorno);
            Assert.False(registro.EhValido());
            Assert.True(registro.ValidationResult.Errors.Count == 3);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(registro.Id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(registro), Times.Never);
        }

        #endregion Atualizar

        #region Remover       
        [Fact(DisplayName = "Remover com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarPorId(registro.Id))
                .Returns(Task.FromResult(registro));

            // Act
            var retorno = await _pedidoService.Remover(registro.Id);

            // Assert
            Assert.True(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarPorId(registro.Id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Remover(registro.Id), Times.Once);

        }

        [Fact(DisplayName = "Remover com Falha já Existente")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Remover_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            Pedido registro = null;
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarPorId(id))
                .ReturnsAsync(registro);

            // Act
            var retorno = await _pedidoService.Remover(id);

            // Assert
            Assert.False(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarPorId(id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Remover(id), Times.Never);

        }

        [Fact(DisplayName = "Remover com Falha Registro Não Aberto")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Remover_DeveExecutarComFalhaNaoAberto()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();
            registro.Status = StatusPedido.Faturado;
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarPorId(id))
                .ReturnsAsync(registro);

            // Act
            var retorno = await _pedidoService.Remover(id);

            // Assert
            Assert.False(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarPorId(id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Remover(id), Times.Never);

        }
        #endregion Remover

        #region Obter Todos
        [Fact(DisplayName = "Obter Todos com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_ObterTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var filtro = HelpersDefault.GerarFiltro();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.ObterTodos(filtro))
                .ReturnsAsync(new ResponseModel<Pedido>(_pedidoTestsAutoMockerFixture.ObterVariados().ToList(), 1000));

            // Act
            var list = await _pedidoService.ObterTodos(filtro);

            // Assert 
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.ObterTodos(filtro), Times.Once);
            Assert.True(list.Data.Any());
        }
        #endregion Obter Todos

        #region Obter Por Id
        [Fact(DisplayName = "Obter por Id com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_ObterPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.ObterPorId(id))
                .Returns(Task.FromResult(_pedidoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _pedidoService.ObterPorId(id);

            // Assert 
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.ObterPorId(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter Por Id

        #region Obter
        [Fact(DisplayName = "Obter com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Obter_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(id))
                .Returns(Task.FromResult(_pedidoTestsAutoMockerFixture.GerarRegistroValido()));

            // Act
            var registro = await _pedidoService.Obter(id);

            // Assert 
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(id), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Obter

        #region Buscar
        [Fact(DisplayName = "Buscar com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Buscar_DeveExecutarComSucesso()
        {
            // Arrange
            Expression<Func<Models.Pedido, bool>> predicate = (x) => x.Codigo > 0;
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Buscar(predicate))
                .Returns(Task.FromResult(_pedidoTestsAutoMockerFixture.ObterVariados()));

            // Act
            var registro = await _pedidoService.Buscar(predicate);

            // Assert 
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Buscar(predicate), Times.Once);
            Assert.True(registro != null);
        }
        #endregion Buscar

        #region Cancelar       
        [Fact(DisplayName = "Cancelar com Sucesso")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Cancelar_DeveExecutarComSucesso()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(registro.Id))
                .Returns(Task.FromResult(registro));

            // Act
            var retorno = await _pedidoService.Cancelar(registro.Id);

            // Assert
            Assert.True(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(registro.Id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(registro), Times.Once);

        }

        [Fact(DisplayName = "Cancelar com Falha já Existente")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Cancelar_DeveExecutarComFalhaJaExistente()
        {
            // Arrange
            Pedido registro = null;
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(id))
                .ReturnsAsync(registro);

            // Act
            var retorno = await _pedidoService.Cancelar(id);

            // Assert
            Assert.False(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(registro), Times.Never);

        }

        [Fact(DisplayName = "Cancelar com Falha Registro Não Aberto")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_Cancelar_DeveExecutarComFalhaNaoAberto()
        {
            // Arrange
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroInvalido();
            registro.Status = StatusPedido.Faturado;
            var id = Guid.NewGuid();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.Obter(id))
                .ReturnsAsync(registro);

            // Act
            var retorno = await _pedidoService.Cancelar(id);

            // Assert
            Assert.False(retorno);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Obter(id), Times.Once);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(registro), Times.Never);

        }
        #endregion Cancelar

        #region Recuperar Quantidade
        [Fact(DisplayName = "Recuperar Quantidade")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_RecuperarQuantidade_DeveExecutarComSucesso()
        {
            // Arrange
            var quantidade = 10;
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarQuantidade())
                .ReturnsAsync(quantidade);

            // Act
            var retorno = await _pedidoService.RecuperarQuantidade();

            // Assert             
            Assert.True(retorno > 0);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarQuantidade(), Times.Once);
        }
        #endregion Recuperar Quantidade

        #region Recuperar Por Id
        [Fact(DisplayName = "Recuperar Por Id")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_RecuperarPorId_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var registro = _pedidoTestsAutoMockerFixture.GerarRegistroValido();
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarPorId(id))
                .ReturnsAsync(registro);

            // Act
            var retorno = await _pedidoService.RecuperarPorId(id);

            // Assert             
            Assert.True(retorno != null);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarPorId(id), Times.Once);
        }
        #endregion Recuperar Por Id

        #region Recuperar Por Id
        [Fact(DisplayName = "Recuperar Todos")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_RecuperarTodos_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dataInicial = DateTime.Now;
            var dataFinal = DateTime.Now;
            var clienteId = Guid.NewGuid();
            var status = 0;
            var registro = _pedidoTestsAutoMockerFixture.GerarList(50, true);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarTodos(dataInicial, dataFinal, clienteId, status))
                .ReturnsAsync(registro.ToList());

            // Act
            var retorno = await _pedidoService.RecuperarTodos(dataInicial, dataFinal, clienteId, status);

            // Assert             
            Assert.True(retorno.Count > 0);
            _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Verify(r => r.RecuperarTodos(dataInicial, dataFinal, clienteId, status), Times.Once);
        }
        #endregion Recuperar Por Id

        #region Recuperar Por Mes
        [Fact(DisplayName = "Recuperar Por Mes")]
        [Trait("Categoria", "Pedido Service Tests")]
        public async void PedidoService_RecuperarPorMes_DeveExecutarComSucesso()
        {
            // Arrange

            var mesAtual = new List<double>();

            var dataInicialFiltro = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                1,
                0,
                0,
                0);
            var dataCorrente = dataInicialFiltro;

            // 12 meses do ano atual
            for (int i = 1; i <= 12; i++)
            {
                DateTime dataInicial =
                    new DateTime(
                    dataCorrente.Year,
                    i,
                    1,
                    0,
                    0,
                    0);
                DateTime dataFinal =
                    new DateTime(
                    dataInicial.Year,
                    dataInicial.Month,
                    DateTime.DaysInMonth(dataInicial.Year, dataInicial.Month),
                    23,
                    59,
                    59);

                var list = _pedidoTestsAutoMockerFixture.GerarList(1, true);

                _pedidoTestsAutoMockerFixture.Mocker.GetMock<IPedidoRepository>().Setup(c => c.RecuperarTodos(dataInicial, dataFinal, Guid.Empty, (int)StatusPedido.Faturado))
                    .ReturnsAsync(list.ToList());

                mesAtual.Add(10);

                dataCorrente = dataCorrente.AddDays(1);
            }

            // Act
            var retorno = await _pedidoService.RecuperarPorMes();

            // Assert             
            Assert.True(retorno.Count == 12);
            Assert.True(retorno.Sum() == mesAtual.Sum());
        }
        #endregion Recuperar Por Mes
    }
}
