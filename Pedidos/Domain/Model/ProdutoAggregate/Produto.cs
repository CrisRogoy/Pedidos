using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Domain.Model.ProdutoAggregate
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; private set; }
        public string Descricao { get; set; }
        public string CodEan { get; set; }
        public string Marca { get; set; }
        public string UnMedida { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal MargemBruta { get; set; }
        public double Estoque { get; set; }
    }
}