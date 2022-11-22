using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Security.Cryptography;

namespace Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository)
        {
            _repository = repository;
        }


        public async Task<int> InserirProduto(Produto produto)
        {
            return await _repository.InserirProduto(produto);

        }

        public async Task<Produto> BuscarProduto(string pid)
        {
            return await _repository.RecuperarProduto(pid);

        }

        public async Task<int> AtualizarProduto(Produto produto)
        {
            return await _repository.AtualizarProduto(produto);

        }

        public async Task<int> ApagarProduto(string pid)
        {
            return await _repository.ApagarProduto(pid);

        }

        public async Task SalvarNoBanco(Produto produto)
        {
            await _repository.SalvarDoBanco(produto);
        }
        public async Task SicronizarDelete(string cid)
        {
            await _repository.DeletarLocalDoBanco(cid);
        }

        public async  Task<List<Produto>> BuscarProdutos()
        {
            return await _repository.RecuperarProdutos();
        }
        public async Task AlterarQuantidade(Produto produto)
        {
             await _repository.AlterarQuantidade(produto);
        }
    }
}
