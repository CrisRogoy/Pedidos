using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.DTos;
using Pedidos.Domain.Enums;
using Pedidos.Domain.Model.PedidoAggregate;
using Pedidos.Domain.Model.ProdutoAggregate;

namespace Pedidos.Infraestrutura.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ConecBanco _con = new();

        public async Task<object> AddItensPedido(ItensPedidoDTo pItensPedido)
        {
            Pedido pedido = _con.Pedidos.FirstOrDefault(p => p.Id == pItensPedido.IdPedido)!;

            if (pedido is null)
            {
                return new
                {
                    Msg = "Pedido não encontrado."
                };
            }

            if ((int)pedido.Status == 1 || (int)pedido.Status == 2)
            {
                return new
                {
                    Msg = "Não possível adicionar itens em pedidos com status (Pronto ou Fechado)."
                };
            }

            Produto? prod = await _con.Produtos.FirstOrDefaultAsync(p => p.Id == pItensPedido.IdProduto);
            if (prod is null)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Produto não encontrado !."
                };
            }

            if (prod.Estoque <= 0)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Produto sem estoque para venda !."
                };
            }

            if ((prod.Estoque - pItensPedido.Quantidade) < 0)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = $"Produto com estoque {prod.Estoque:F3}\nQuantidade vendida excede o estoque do produto."
                };
            }

            prod.Estoque -= pItensPedido.Quantidade;

            ItensPedido item = new()
            {
                IdProduto = prod.Id,
                IdPedido = pItensPedido.IdPedido,
                Quantidade = pItensPedido.Quantidade,
                VlrUnitario = pItensPedido.VlrUnitario,
                VlrDesconto = pItensPedido.VlrDesconto,
                VlrTotal = pItensPedido.VlrTotal
            };

            _con.ItensPedidos.Add(item);

            _con.Produtos.Update(prod);

            await _con.SaveChangesAsync();

            return new
            {
                Msg = item.Id.ToString()
            };
        }

        public async Task<object> AddPedido(PedidoDTo pPedido)
        {
            Pedido pedido = new()
            {
                Status = 0,
                CpfCliente = pPedido.CpfCliente,
                DtAlteradoStatus = DateTime.Now,
                DtCriacao = DateTime.Now
            };

            _con.Pedidos.Add(pedido);

            await _con.SaveChangesAsync();

            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Msg = pedido.Id.ToString()
            };
        }

        public async Task<object> DeleteItemPedido(int pIdPedido, int pIdItemPedido)
        {
            Pedido? ped = await _con.Pedidos.FirstOrDefaultAsync(x => x.Id == pIdPedido);

            if (ped is not null)
            {
                if (ped.Status == 0)
                {
                    List<ItensPedido> lItensPedido = await _con.ItensPedidos
                                                        .Where(x => x.IdPedido == pIdPedido && x.Id == pIdItemPedido)
                                                        .ToListAsync();

                    if (lItensPedido.Count > 0)
                    {
                        ItensPedido? iItem = lItensPedido.FirstOrDefault(x => x.Id == pIdItemPedido);
                        if (iItem is not null)
                        {
                            _con.ItensPedidos.Remove(iItem);
                            await _con.SaveChangesAsync();

                            return new
                            {
                                CodRet = Guid.NewGuid().ToString(),
                                Msg = "Item removido com sucesso."
                            };
                        }
                        else
                        {
                            return new
                            {
                                CodRet = Guid.NewGuid().ToString(),
                                Msg = "Item não encontrado."
                            };
                        }
                    }
                    else
                    {
                        return new
                        {
                            CodRet = Guid.NewGuid().ToString(),
                            Msg = "Item não encontrado."
                        };
                    }
                }
                else
                {
                    return new
                    {
                        CodRet = Guid.NewGuid().ToString(),
                        Msg = "Não possivel alterar pedidos que já estao fechados ou prontos."
                    };
                }
            }
            else
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Pedido não encontrado."
                };
            }
        }

        public async Task<object> GetPedidosSemItens(StatusPedido? pStatus = null, int pPagina = 1, int pItensPorPagina = 10)
        {
            // Query base
            IQueryable<Pedido> lPedidosBanco = _con.Pedidos.AsQueryable();

            // Aplica filtro apenas se status foi informado
            if (pStatus.HasValue)
            {
                lPedidosBanco = lPedidosBanco.Where(x => x.Status == pStatus.Value);
            }

            // Total de registros (com ou sem filtro)
            int iTotalRegistros = await lPedidosBanco.CountAsync();

            // Busca paginada
            List<Pedido> lPedidos = await lPedidosBanco
                                        .OrderBy(x => x.Id)
                                        .Skip((pPagina - 1) * pItensPorPagina)
                                        .Take(pItensPorPagina)
                                        .ToListAsync();

            // Calcula total de páginas
            int iTotalPaginas = (int)Math.Ceiling(iTotalRegistros / (double)pItensPorPagina);

            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Dados = lPedidos,
                Paginacao = new
                {
                    PaginaAtual = pPagina,
                    ItensPorPagina = pItensPorPagina,
                    TotalRegistros = iTotalRegistros,
                    TotalPaginas = iTotalPaginas,
                    FiltroStatus = pStatus?.ToString() // Mostra qual filtro foi aplicado
                },
                Msg = "Lista de pedidos retornada com sucesso."
            };
        }

        public async Task<object> GetPedidoByIdComItens(int pId)
        {
            Pedido? Ped = await _con.Pedidos
                                         .Include(x => x.ItensPedido)
                                         .FirstOrDefaultAsync(x => x.Id == pId);


            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Dados = Ped,
                Msg = "Lista de pedidos retornada com sucesso."
            };
        }

        public async Task<object> UpdateStatusPedido(int pIdPedido, StatusPedido pStatus)
        {
            Pedido pedido = _con.Pedidos.Include(x => x.ItensPedido).FirstOrDefault(p => p.Id == pIdPedido)!;

            if (pedido is null)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Pedido não encontrado."
                };
            }

            if ((int)pStatus < 0 || (int)pStatus > 2)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Status inválido."
                };
            }

            if (pedido.ItensPedido.Count == 0 && pStatus == StatusPedido.Fechado)
            {
                return new
                {
                    CodRet = Guid.NewGuid().ToString(),
                    Msg = "Pedido não contém itens, então não pode ser alterado status para (Fechado)."
                };
            }

            pedido.Status = pStatus;
            pedido.DtAlteradoStatus = DateTime.Now;
            _con.Pedidos.Update(pedido);

            await _con.SaveChangesAsync();

            return new
            {
                CodRet = Guid.NewGuid().ToString(),
                Msg = "Status do pedido atualizado com sucesso."
            };
        }
    }
}