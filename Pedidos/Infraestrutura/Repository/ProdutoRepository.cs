using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.DTos;
using Pedidos.Domain.Model.PedidoAggregate;
using Pedidos.Domain.Model.ProdutoAggregate;
using System.Net.NetworkInformation;

namespace Pedidos.Infraestrutura.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ConecBanco _con = new();

        public async Task<object> AddOrUpdateProduto(ProdutoDTo pProduto)
        {
            Produto? cProd = _con.Produtos.FirstOrDefault(p => p.Id == pProduto.Id);
            object? oRet = null;

            if (cProd is not null)
            {
                cProd.PrecoVenda = pProduto.PrecoVenda;
                cProd.PrecoCusto = pProduto.PrecoCusto;
                cProd.MargemBruta = pProduto.MargemBruta;
                cProd.UnMedida = pProduto.UnMedida;
                cProd.Estoque = pProduto.Estoque;
                cProd.Descricao = pProduto.Descricao;
                cProd.CodEan = pProduto.CodEan;
                cProd.Marca = pProduto.Marca;
                _con.Produtos.Update(cProd);

                await _con.SaveChangesAsync();

                oRet = new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    CodProdutoAtualizado = cProd.Id,
                    Msg = "Produto atualizado !."
                };
            }
            else
            {
                Produto produto = new()
                {
                    Descricao = pProduto.Descricao,
                    CodEan = pProduto.CodEan,
                    Marca = pProduto.Marca,
                    UnMedida = pProduto.UnMedida,
                    PrecoVenda = pProduto.PrecoVenda,
                    PrecoCusto = pProduto.PrecoCusto,
                    MargemBruta = pProduto.MargemBruta,
                    Estoque = pProduto.Estoque
                };
                _con.Produtos.Add(produto);

                await _con.SaveChangesAsync();

                oRet = new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    CodProdutoInserido = produto.Id,
                    Msg = "Produto inserido !."
                };
            }

            return oRet;
        }

        public async Task<object> DeleteProduto(int pIdProd)
        {
            Produto? pProd = await _con.Produtos.FirstOrDefaultAsync(x => x.Id == pIdProd);
            if (pProd is null)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = $"Id produto : {pIdProd} não encontrado !."
                };
            }
            _con.Produtos.Remove(pProd);
            await _con.SaveChangesAsync();

            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Msg = $"Produto {pIdProd} removido com sucesso !"
            };
        }

        public async Task<object> GetAllProdutos(int pPagina = 1, int pItensPorPagina = 10)
        {
            // Query base
            IQueryable<Produto> lProdutosBanco = _con.Produtos.AsQueryable();

            int iTotalRegistros = await lProdutosBanco.CountAsync();

            // Busca paginada
            List<Produto> lProdutos = await lProdutosBanco
                                        .OrderBy(x => x.Id)
                                        .Skip((pPagina - 1) * pItensPorPagina)
                                        .Take(pItensPorPagina)
                                        .ToListAsync();

            // Calcula total de páginas
            int iTotalPaginas = (int)Math.Ceiling(iTotalRegistros / (double)pItensPorPagina);

            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Dados = lProdutos,
                Paginacao = new
                {
                    PaginaAtual = pPagina,
                    ItensPorPagina = pItensPorPagina,
                    TotalRegistros = iTotalRegistros,
                    TotalPaginas = iTotalPaginas
                },
                Msg = "Lista de produtos retornada com sucesso."
            };
        }
    }
}