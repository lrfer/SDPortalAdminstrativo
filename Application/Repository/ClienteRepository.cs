using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        List<Cliente> clientesCache;
        List<Cliente> clientesBanco;
        public ClienteRepository()
        {
            this.clientesCache = new();
            this.clientesBanco = new();
        }

        public async Task<int> ApagarCliente(string cid)
        {
            var cli = await RecuperarCliente(cid);
            if (cli is not null)
            {
                clientesCache.Remove(cli);
                await EnviarDelete(cid);
                return 1;
            }
            else
                return 0;
        }

        public async Task<int> AtualizarCliente(Cliente cliente)
        {
            SyncBanco();
            var cli = await RecuperarCliente(cliente.ClientId);
            if (cli is not null)
            {
                clientesCache.FirstOrDefault(x=> x.ClientId == cli.ClientId);
                await SalvarNoBanco(cliente);
                return 1;
            }
            else
                return 0;
        }

        public async Task<int> InserirCLiente(Cliente cliente)
        {
            var cli = await RecuperarCliente(cliente.ClientId);
            if (cli is null)
            {
                clientesCache.Add(cliente);
                await SalvarNoBanco(cliente);
                return 1;
            }
            else
                return 0;
                
        }

        public async Task<Cliente> RecuperarCliente(string cid)
        {
            var clienteLocal = clientesCache.FirstOrDefault(x => x.ClientId == cid);
            if (clienteLocal is null)
            {
                var clienteBanco = clientesBanco.FirstOrDefault(x => x.ClientId == cid);
                if (clienteBanco is null)
                    return null;
                else
                {
                    SyncBanco();
                    return clienteBanco;
                }

            }
            else
                return clienteLocal;
        }

        public async Task SalvarDoBanco(Cliente cliente)
        {
            var clienteLocal = clientesBanco.FirstOrDefault(x => x.ClientId == cliente.ClientId);
            if (clienteLocal is null)
                clientesBanco.Add(cliente);
            else
                clienteLocal.Name = cliente.Name;
        }

        public async Task DeletarLocalDoBanco(string cid)
        {
            var clienteLocal = clientesBanco.FirstOrDefault(x => x.ClientId == cid);
            if (clienteLocal is not null)
            {
                clientesCache.Remove(clienteLocal);
                clientesBanco.Remove(clienteLocal);
            }

        }

        private async Task SalvarNoBanco(Cliente cliente)
        {
            string json = JsonSerializer.Serialize(cliente);
            await MqttServicePublisher.MqttServiceSendMsg(json, Const.QueueCliente);
        }

        private void SyncBanco()
        {
            clientesCache = new();
            foreach (var cliente in clientesBanco)
                clientesCache.Add(cliente);
        }

        private async Task EnviarDelete(string cid)
        {
            await MqttServicePublisher.MqttServiceSendMsg(cid, Const.QueueClienteDelete);
        }


    }
}
