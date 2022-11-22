using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<int> InserirCLiente(Cliente cliente);
        Task<int> AtualizarCliente(Cliente cliente);
        Task<int> ApagarCliente(string cid);
        Task<Cliente> RecuperarCliente(string cid);
        Task DeletarLocalDoBanco(string cid);
        Task SalvarDoBanco(Cliente cliente);
    }
}
