using Application.Services.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client;

using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class MqttServiceConsumer : BackgroundService, IDisposable
    {
        private readonly IClienteService _clienteService;
        private readonly IProdutoService _produtoService;
        IMqttClient _client;

        MqttClientOptions _clientOptions;
        MqttClientSubscribeOptions _subscriptionOptions;

        public MqttServiceConsumer(IClienteService clienteService, IProdutoService produtoService)
        {
            _clienteService = clienteService;
            _produtoService = produtoService;


            var mqttFactory = new MqttFactory();

            _client = mqttFactory.CreateMqttClient();

            _clientOptions = new MqttClientOptionsBuilder()
                              .WithClientId(Const.ClientId.ToString())
                              .WithTcpServer(Const.ConnectionMqtt, Const.ConnectionMqttPort)
                              .Build();



            _subscriptionOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                        .WithTopicFilter(f =>
                        {
                            f.WithTopic(Const.QueueCliente);

                        })
                        .WithTopicFilter(f =>
                        {
                            f.WithTopic(Const.QueueProduto);
                        })
                        .WithTopicFilter(f =>
                        {
                            f.WithTopic(Const.QueueClienteDelete);
                        })
                        .WithTopicFilter(f =>
                        {
                            f.WithTopic(Const.QueueProdutosDelete);
                        })
                        .WithTopicFilter(f =>
                        {
                            f.WithTopic(Const.QueueProdutoRemover);
                        })
                        .Build();


            _client.ApplicationMessageReceivedAsync += HandleMessageAsync;
        }



        async Task HandleMessageAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            if (e.ApplicationMessage.Topic.Equals(Const.QueueProduto))
            {
                var result = JsonSerializer.Deserialize<Produto>(payload);
                if (result is not null)
                    await _produtoService.SalvarNoBanco(result);
            }
            if (e.ApplicationMessage.Topic.Equals(Const.QueueCliente))
            {
                var result = JsonSerializer.Deserialize<Cliente>(payload);
                if (result is not null)
                    await _clienteService.SalvarNoBanco(result);
            }
            if (e.ApplicationMessage.Topic.Equals(Const.QueueClienteDelete))
                    await _clienteService.SicronizarDelete(payload);
            
            if (e.ApplicationMessage.Topic.Equals(Const.QueueProdutosDelete))
                    await _produtoService.SicronizarDelete(payload);

            if (e.ApplicationMessage.Topic.Equals(Const.QueueProdutoRemover))
            {
                var result = JsonSerializer.Deserialize<Produto>(payload);
                if(result is not null)
                    await _produtoService.AlterarQuantidade(result);
            }

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _client.ConnectAsync(_clientOptions, CancellationToken.None);

            await _client.SubscribeAsync(_subscriptionOptions, CancellationToken.None);
        }
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.DisconnectAsync();
            await base.StopAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client.Dispose();
                base.Dispose();
            }
            _client = null;
        }
    }
}
