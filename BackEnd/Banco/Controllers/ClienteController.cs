using Banco.Interface;
using Banco.Models;
using Banco.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// Método responsavel por listar todos os clientes cadastrados no banco de dados
        /// </summary>
        /// <returns>A lista de clientes existentes</returns>
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {

                return Ok(_clienteRepository.ListarTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Ocorreu um erro ao listar os clientes: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsavel por buscar um cliente específico através do seu id, caso o cliente exista, ele é retornado
        /// </summary>
        /// <param name="id">O cliente para o id selecionado</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult BuscarClientePorId(int id)
        {
            try
            {
                return Ok(_clienteRepository.BuscarClientePorId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Ocorreu um erro ao buscar o cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsavel por cadastrar um cliente no banco de dados, recebendo um objeto do tipo cliente, contendo as informações necessárias para o cadastro
        /// </summary>
        /// <param name="cliente">As informações do cliente a ser cadastrado</param>
        [HttpPost]
        public IActionResult CadastrarCliente(ClienteDTO cliente)
        {
            try
            {
                _clienteRepository.CadastrarCliente(cliente);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao cadastrar o cliente: {ex.Message}");
            }


        }

        /// <summary>
        /// método responsavel por deletar um cliente do banco de dados, recebendo o id do cliente a ser deletado, caso o cliente exista, ele é deletado do banco de dados
        /// </summary>
        /// <param name="id">Id do cliente que sera deletado</param>
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            try
            {
                _clienteRepository.DeletarCliente(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao deletar o cliente: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Método responsavel por atualizar as informações de um cliente, recebendo um objeto do tipo cliente, contendo as informações atualizadas do cliente, e o id do cliente a ser atualizado
        /// </summary>
        /// <param name="cliente">Recebe as novas informações a serem atualizadas</param>
        /// <param name="id">O id do cliente que vai ter informações atualizadas</param>
        [HttpPut("{id}")]
        public IActionResult UpdateInfoCliente([FromBody] ClienteViewModel cliente, int id)
        {
            try
            {
                _clienteRepository.UpdateInfoCliente(cliente, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ocorreu um erro ao atualiza as informações do cliente: {ex.Message}");
            }
        }

        /// <summary>
        /// Método responsavel por realizar saques na conta do cliente
        /// </summary>
        /// <param name="valor">Valor da operação</param>
        /// <param name="id">Id do cliente</param>
        [HttpPut("{id}/sacar")]
        public IActionResult Sacar([FromBody] SaldoDTO valor, int id)
        {
            try
            {
                _clienteRepository.Sacar(valor, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ocorreu um erro durante o saque: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Método responsavel por realizar depositos na conta do cliente
        /// </summary>
        /// <param name="valor">Valor da operação</param>
        /// <param name="id">Id do cliente</param>
        [HttpPut("{id}/depositar")]
        public IActionResult Depositar([FromBody] SaldoDTO valor, int id)
        {
            try
            {
                _clienteRepository.Depositar(valor, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ocorreu um erro durante o deposito: {ex.Message}");
            }
            
        }
    }
}
