using Pedidos.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Domain.Model.PedidoAggregate
{
    [Table("Pedidos")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; private set; }
        public DateTime DtCriacao { get; set; }
        public DateTime DtAlteradoStatus { get; set; }
        public StatusPedido Status { get; set; } // 0 = Adicionado, 1 = Fechado, 2 = Pronto
        public string? CpfCliente { get; set; }
        public List<ItensPedido> ItensPedido { get; set; } = new();

    }
}