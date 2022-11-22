using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> InserirCLiente(Cliente cliente)
        {
            var result =  await _repository.InserirCLiente(cliente);
            return result;
        }

        public async Task <Cliente> BuscarCliente(string cid)
        {
            return await _repository.RecuperarCliente(cid);
        }

        public async Task<int> AtualizarCliente(Cliente cliente)
        {
            return await _repository.AtualizarCliente(cliente);
        }

        public async Task<int> ApagarCliente(string cid)
        {
            return await _repository.ApagarCliente(cid);
        }

        public async Task SalvarNoBanco(Cliente cliente)
        {
            await _repository.SalvarDoBanco(cliente);
        }

        public async Task SicronizarDelete(string cid)
        {
            await _repository.DeletarLocalDoBanco(cid);
        }
    }
}
