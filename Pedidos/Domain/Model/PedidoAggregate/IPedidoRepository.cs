using Pedidos.Domain.DTos;
using Pedidos.Domain.Enums;

namespace Pedidos.Domain.Model.PedidoAggregate
{
    public interface IPedidoRepository
    {
        Task<object> AddPedido(PedidoDTo pPedido);
        Task<object> AddItensPedido(ItensPedidoDTo pItensPedido);
        Task<object> DeleteItemPedido(int pIdPedido, int pIdItemPedido);
        Task<object> UpdateStatusPedido(int pIdPedido, StatusPedido pStatus);
        Task<object> GetPedidosSemItens(StatusPedido? pStatus, int pPagina = 1, int pItensPorPagina = 10);
        Task<object> GetPedidoByIdComItens(int pId);
    }
}