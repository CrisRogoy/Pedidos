using Pedidos.Domain.Enums;

namespace Pedidos.Domain.DTos
{
    public class PedidoDTo
    {
        public DateTime DtCriacao { get; set; }
        public DateTime DtAlteradoStatus { get; set; }
        public StatusPedido Status { get; set; } // 0 = Adicionado, 1 = Fechado, 2 = Pronto
        public string? CpfCliente { get; set; }
    }
}