using Banco.Context;
using Banco.Interface;
using Banco.Models;
using Banco.ViewModels;

namespace Banco.Repositoris
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BancoContext _context;
        private readonly ILogger<ClienteRepository> _logger;

        public ClienteRepository(BancoContext context, ILogger<ClienteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Cliente BuscarClientePorId(int id)
        {
            Cliente clienteBuscado = _context.Clientes.FirstOrDefault(c => c.Id == id);

            if (clienteBuscado != null)
            {
                _logger.LogInformation("O cliente {ClienteId} foi encontrado na nossa base de dados",id);
                return clienteBuscado;
            }
            else
            {
                _logger.LogError("O cliente {ClienteId} não foi encontrado na nossa base de dados",id);
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
        }

        public void CadastrarCliente(ClienteDTO cliente)
        {
            Cliente clienteCadastro = new Cliente();

            clienteCadastro.Nome = cliente.Nome;
            clienteCadastro.Email = cliente.Email;

            _context.Clientes.Add(clienteCadastro);
            _context.SaveChanges();
            _logger.LogInformation("O cliente {Nome} foi cadastrado com sucesso",cliente.Nome);
        }

        public void DeletarCliente(int id)
        {
            Cliente clienteBuscado = BuscarClientePorId(id);
            _context.Clientes.Remove(clienteBuscado);
            _context.SaveChanges();
            _logger.LogInformation("O cliente {id} foi removido com sucesso", id);
        }

        public List<Cliente> ListarTodos()
        {
            return _context.Clientes.ToList();
        }

        public void Sacar(SaldoDTO valor, int id)
        {
            Cliente clienteBuscado = BuscarClientePorId(id);

            float valorSaque = valor.Saldo;

            if (valorSaque <= 0)
            {
                _logger.LogError("O cliente {ClienteId} informou um valor ({valor}) inválido para sacar", id, valorSaque);
                throw new InvalidOperationException("Valor inválido para a operação.");
            }

            if (clienteBuscado.Saldo >= valorSaque)
            {
                clienteBuscado.Saldo -= valorSaque;

                _context.SaveChanges();
                _logger.LogInformation("Saque realizado com sucesso para o cliente {id}", id);
            }
            else
            {
                _logger.LogWarning("O cliente {ClienteId} tem um saldo insuficiente para realizar o saque", id);
                throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");
            }
        }
        public void Depositar(SaldoDTO valor, int id)
        {
            Cliente clienteBuscado = BuscarClientePorId(id);
            float valorDeposito = valor.Saldo;

            if (valorDeposito <= 0)
            {
                _logger.LogError("O cliente {ClienteId} informou um valor ({valor}) inválido para depositar", id, valorDeposito);
                throw new InvalidOperationException("Valor inválido para a operação.");
            }

            clienteBuscado.Saldo += valorDeposito;
            _context.SaveChanges();
            _logger.LogInformation("Deposito realizado com sucesso para o cliente {id}", id);
        }

        public void UpdateInfoCliente(ClienteViewModel cliente, int id)
        {
            Cliente clienteBuscado = BuscarClientePorId(id);

            if (cliente.Nome != null)
            {
                clienteBuscado.Nome = cliente.Nome;
            } 
            
            if (cliente.Email != null)
            {
                clienteBuscado.Email = cliente.Email;
            }

            _logger.LogInformation("Informações do cliente {id} atualizadas com sucesso", id);
            _context.Clientes.Update(clienteBuscado);
            _context.SaveChanges();
        }
    }
}
