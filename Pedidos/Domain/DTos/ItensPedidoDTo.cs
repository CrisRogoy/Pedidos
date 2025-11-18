namespace Pedidos.Domain.DTos
{
    public class ItensPedidoDTo
    {
        public int IdProduto { get; set; }
        public int IdPedido { get; set; }    
        public double Quantidade { get; set; }
        public decimal VlrUnitario { get; set; }
        public decimal VlrDesconto { get; set; }
        public decimal VlrTotal { get; set; }
    }
}