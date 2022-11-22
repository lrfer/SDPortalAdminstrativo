using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IClienteService
    {
        Task<int> InserirCLiente(Cliente cliente);
        Task<Cliente> BuscarCliente(string id);
        Task<int> AtualizarCliente(Cliente cliente);
        Task<int> ApagarCliente(string id);

        Task SalvarNoBanco(Cliente cliente);

        Task SicronizarDelete(string cid);

    }
}
