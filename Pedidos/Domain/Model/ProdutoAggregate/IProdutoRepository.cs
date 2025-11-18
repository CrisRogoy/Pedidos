using Pedidos.Domain.DTos;

namespace Pedidos.Domain.Model.ProdutoAggregate
{
    public interface IProdutoRepository
    {
        Task<object> AddOrUpdateProduto(ProdutoDTo pProduto);
        Task<object> DeleteProduto(int pIdProd);
        Task<object> GetAllProdutos(int pPagina = 1, int pItensPorPagina = 10);
    }
}