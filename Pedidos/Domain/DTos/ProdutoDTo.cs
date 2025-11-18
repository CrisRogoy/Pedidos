namespace Pedidos.Domain.DTos
{
    public class ProdutoDTo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string UnMedida { get; set; }
        public string CodEan { get; set; }
        public string Marca { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal MargemBruta { get; set; }
        public int Estoque { get; set; }
    }
}