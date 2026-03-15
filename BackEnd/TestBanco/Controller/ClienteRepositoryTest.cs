using Banco.Controllers;
using Banco.Interface;
using Banco.Models;
using Banco.Repositoris;
using Banco.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Banco.UnitTest.Repository
{
    public class ClienteRepositoryTest
    {
        [Fact(DisplayName = "Deve listar todos os clientes")]
        public void ListarClientes()
        {
            var listaClientes = new List<Cliente>();

            var cli1 = new Cliente { Id = 1, Nome = "Cliente 1", Email = "teste@teste.com", Saldo = 1000 };
            var cli2 = new Cliente { Id = 2, Nome = "Cliente 2", Email = "teste@teste.com", Saldo = 1000 };
            var cli3 = new Cliente { Id = 3, Nome = "Cliente 3", Email = "teste@teste.com", Saldo = 1000 };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository.Setup(x => x.ListarTodos()).Returns(listaClientes);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.ListarTodos();

            Assert.IsType<OkObjectResult>(resultado);

        }

        [Fact(DisplayName = "Deve listar um cliente especifico")]
        public void BuscarClientePorId()
        {

            var cli1 = new Cliente { Id = 1, Nome = "Cliente 1", Email = "teste@teste.com", Saldo = 1000 };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository.Setup(x => x.BuscarClientePorId(cli1.Id)).Returns(cli1);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.BuscarClientePorId(cli1.Id);

            Assert.IsType<OkObjectResult>(resultado);

        }

        [Fact(DisplayName = "Deve exibir o erro que nenhum cliente foi encontrado")]
        public void BuscarClientePorId_ClienteNãoEncontrado()
        {

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository.Setup(x => x.BuscarClientePorId(1))
            .Throws(new KeyNotFoundException("Cliente não encontrado."));

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.BuscarClientePorId(1);

            var objectResult = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(404, objectResult.StatusCode);

        }

        [Fact(DisplayName = "Deve atualizar as informacoes de um cliente especifico")]
        public void AtualizarInfos()
        {

            var cli1 = new ClienteViewModel { Nome = "Cliente 1", Email = "teste@teste.com" };

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Saldo = 1000
            };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository
                .Setup(x => x.BuscarClientePorId(1))
                .Returns(cliente);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.UpdateInfoCliente(cli1, 1);

            Assert.IsType<NoContentResult>(resultado);

        }

        [Fact(DisplayName = "Deve cadastrar um cliente")]
        public void CadastrarCliente()
        {
            var cli1 = new ClienteDTO
            {
                Nome = "Cliente 1",
                Email = "teste@teste.com"
            };

            var mockRepository = new Mock<IClienteRepository>();

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.CadastrarCliente(cli1);

            mockRepository.Verify(x => x.CadastrarCliente(cli1), Times.Once);

            Assert.IsType<CreatedResult>(resultado);
        }

        [Fact(DisplayName = "Deve deletar o cliente")]
        public void Deletar()
        {

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Saldo = 1000
            };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository
                .Setup(x => x.BuscarClientePorId(1))
                .Returns(cliente);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.DeleteCliente(1);

            mockRepository.Verify(x => x.DeletarCliente(1), Times.Once);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact(DisplayName = "Deve realizar saque quando saldo for suficiente")]
        public void Sacar_ComSucesso()
        {
            var saldo = new SaldoDTO { Saldo = 100 };

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Saldo = 1000
            };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository
                .Setup(x => x.BuscarClientePorId(1))
                .Returns(cliente);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.Sacar(saldo, 1);

            mockRepository.Verify(x => x.Sacar(saldo, 1), Times.Once);

            Assert.IsType<NoContentResult>(resultado);
        }

        [Fact(DisplayName = "Deve retornar erro quando saldo for insuficiente")]
        public void Sacar_SaldoInsuficiente()
        {
            var saldo = new SaldoDTO { Saldo = 1000 };

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste",
                Saldo = 500
            };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository
                .Setup(x => x.BuscarClientePorId(1))
                .Returns(cliente);

            mockRepository
                .Setup(x => x.Sacar(saldo, 1))
                .Throws(new InvalidOperationException("Saldo insuficiente para realizar o saque."));

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.Sacar(saldo, 1);

            var objectResult = Assert.IsType<ObjectResult>(resultado);

            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact(DisplayName = "Deve realizar um deposito")]
        public void Depositar_ComSucesso()
        {
            var saldo = new SaldoDTO { Saldo = 100 };

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@teste.com",
                Saldo = 1000
            };

            var mockRepository = new Mock<IClienteRepository>();

            mockRepository
                .Setup(x => x.BuscarClientePorId(1))
                .Returns(cliente);

            var controller = new ClienteController(mockRepository.Object);

            var resultado = controller.Depositar(saldo, 1);

            mockRepository.Verify(x => x.Depositar(saldo, 1), Times.Once);

            Assert.IsType<NoContentResult>(resultado);
        }
    }
}
