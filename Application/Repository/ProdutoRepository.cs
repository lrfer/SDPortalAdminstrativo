using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        List<Produto> produtosCache;
        List<Produto> produtosBanco;
        public ProdutoRepository()
        {
            this.produtosCache = new();
            this.produtosBanco = new();
        }

        public async Task<int> ApagarProduto(string cid)
        {
            var cli = await RecuperarProduto(cid);
            if (cli is not null)
            {
                produtosCache.Remove(cli);
                await EnviarDelete(cid);
                return 1;
            }
            else
                return 0;
        }

        public async Task<int> AtualizarProduto(Produto produto)
        {
            SyncBanco();
            var cli = await RecuperarProduto(produto.ProdutctId);
            if (cli is not null)
            {
                produtosCache.FirstOrDefault(x=> x.ProdutctId == cli.ProdutctId);
                await SalvarNoBanco(produto);
                return 1;
            }
            else
                return 0;
        }

        public async Task<int> InserirProduto(Produto produto)
        {
            var cli = await RecuperarProduto(produto.ProdutctId);
            if (cli is null)
            {
                produtosCache.Add(produto);
                await SalvarNoBanco(produto);
                return 1;
            }
            else
                return 0;
                
        }

        public async Task<Produto> RecuperarProduto(string cid)
        {
            var produtoLocal = produtosCache.FirstOrDefault(x => x.ProdutctId == cid);
            var produtoBanco = produtosBanco.FirstOrDefault(x => x.ProdutctId == cid);
            if (produtoLocal is null)
            {
                if (produtoBanco is null)
                    return null;
                else
                {
                    SyncBanco();
                    return produtoBanco;
                }

            }
            else if (produtoBanco is not null)
            {
                if (produtoLocal.Name == produtoBanco.Name && produtoLocal.Price == produtoBanco.Price)
                    return produtoLocal;
                else
                {
                    SyncBanco();
                    return produtoBanco;
                }
            }
            else
                return produtoLocal;
        }

        public async Task AlterarQuantidade(Produto produto)
        {
            var produtoBanco = await RecuperarProduto(produto.ProdutctId);
            produtoBanco.Quantity += produto.Quantity;
            await AtualizarProduto(produtoBanco);
        }

        public async Task SalvarDoBanco(Produto produto)
        {
            var produtoLocal = produtosBanco.FirstOrDefault(x => x.ProdutctId == produto.ProdutctId);
            if (produtoLocal is null)
                produtosBanco.Add(produto);
            else
            {
                produtoLocal.Name = produto.Name;
                produtoLocal.Price = produto.Price;
                produtoLocal.Quantity = produto.Quantity;
            }
        }

        public async Task DeletarLocalDoBanco(string pid)
        {
            //Implementar Queue de delete para sicronizar aqui;
            var produtoLocal = produtosBanco.FirstOrDefault(x => x.ProdutctId == pid);
            if (produtoLocal is not null)
            {
                produtosCache.Remove(produtoLocal);
                produtosBanco.Remove(produtoLocal);
            }

        }

        public Task<List<Produto>> RecuperarProdutos()
        {
            return Task.FromResult(this.produtosBanco);
        }

        #region Metodos privados
        private async Task SalvarNoBanco(Produto produto)
        {
            string json = JsonSerializer.Serialize(produto);
            await MqttServicePublisher.MqttServiceSendMsg(json, Const.QueueProduto);
        }
        private void SyncBanco()
        {
            produtosCache = new();
            foreach (var produto in produtosBanco)
                produtosCache.Add(produto);
        }

        private async Task EnviarDelete(string pid)
        {
            await MqttServicePublisher.MqttServiceSendMsg(pid, Const.QueueProdutosDelete);
        }

        #endregion
    }
}
