using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<int> InserirProduto(Produto produto);
        Task<int> AtualizarProduto(Produto produto);
        Task<int> ApagarProduto(string pid);
        Task<Produto> RecuperarProduto(string pid);
        Task DeletarLocalDoBanco(string pid);
        Task SalvarDoBanco(Produto produto);
        Task<List<Produto>> RecuperarProdutos();
        Task AlterarQuantidade(Produto produto);
    }
}
