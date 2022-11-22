using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<int> InserirProduto(Produto produto);
        Task<Produto> BuscarProduto(string pid);
        Task<List<Produto>> BuscarProdutos();
        Task<int> AtualizarProduto(Produto produto);
        Task<int> ApagarProduto(string pid);
        Task SalvarNoBanco(Produto produto);
        Task SicronizarDelete(string cid);
        Task AlterarQuantidade(Produto result);
    }
}
