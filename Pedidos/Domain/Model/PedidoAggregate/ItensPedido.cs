using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Domain.Model.PedidoAggregate
{
    [Table("ItensPedido")]
    public class ItensPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; private set; }

        [ForeignKey("Pedido")]
        public int IdPedido { get; set; }  // Chave estrangeira
        public Pedido Pedido { get; set; }  // Navegação    
        public double Quantidade { get; set; }
        public decimal VlrUnitario { get; set; }
        public decimal VlrDesconto { get; set; }
        public decimal VlrTotal { get; set; }
        public int IdProduto { get; set; }
    }
}