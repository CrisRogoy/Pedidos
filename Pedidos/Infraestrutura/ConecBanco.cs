using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Model.PedidoAggregate;
using Pedidos.Domain.Model.ProdutoAggregate;
using Pedidos.Domain.Model.UsuariosAggregate;

namespace Pedidos.Infraestrutura
{
    public class ConecBanco : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItensPedido> ItensPedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Pedidos");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}