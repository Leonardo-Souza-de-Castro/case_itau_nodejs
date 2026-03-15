using Banco.Models;
using Banco.ViewModels;

namespace Banco.Interface
{
    public interface IClienteRepository
    {
        /// <summary>
        /// Método responsavel por listar todos os clientes cadastrados no banco de dados
        /// </summary>
        /// <returns>A lista de clientes existentes</returns>
        List<Cliente> ListarTodos();

        /// <summary>
        /// Método responsavel por buscar um cliente específico através do seu id, caso o cliente exista, ele é retornado
        /// </summary>
        /// <param name="id">O cliente para o id selecionado</param>
        /// <returns></returns>
        Cliente BuscarClientePorId(int id);

        /// <summary>
        /// Método responsavel por cadastrar um cliente no banco de dados, recebendo um objeto do tipo cliente, contendo as informações necessárias para o cadastro
        /// </summary>
        /// <param name="cliente">As informações do cliente a ser cadastrado</param>
        void CadastrarCliente(ClienteDTO cliente);

        /// <summary>
        /// Método responsavel por atualizar as informações de um cliente, recebendo um objeto do tipo cliente, contendo as informações atualizadas do cliente, e o id do cliente a ser atualizado
        /// </summary>
        /// <param name="cliente">Recebe as novas informações a serem atualizadas</param>
        /// <param name="id">O id do cliente que vai ter informações atualizadas</param>
        void UpdateInfoCliente(ClienteViewModel cliente, int id);

        /// <summary>
        /// Método responsavel por realizar saques na conta do cliente
        /// </summary>
        /// <param name="valor">Valor da operação</param>
        /// <param name="id">Id do cliente</param>
        void Sacar(SaldoDTO valor, int id);

        /// <summary>
        /// Método responsavel por realizar depositos na conta do cliente
        /// </summary>
        /// <param name="valor">Valor da operação</param>
        /// <param name="id">Id do cliente</param>
        void Depositar(SaldoDTO valor, int id);

        /// <summary>
        /// método responsavel por deletar um cliente do banco de dados, recebendo o id do cliente a ser deletado, caso o cliente exista, ele é deletado do banco de dados
        /// </summary>
        /// <param name="id">Id do cliente que sera deletado</param>
        void DeletarCliente(int id);

    }
}
