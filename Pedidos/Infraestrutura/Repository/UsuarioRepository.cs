using Pedidos.Domain.DTos;
using Pedidos.Domain.Model.UsuariosAggregate;

namespace Pedidos.Infraestrutura.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ConecBanco _con = new();

        public Usuario? GetUsuario(string pEmail, string pSenha)
        {
            // Busca o usuário
            Usuario? usu = _con.Usuarios.FirstOrDefault(x => x.Email == pEmail && x.Senha == pSenha);

            return usu;
        }

        public async Task PostUsuario(UsuarioDto pUsuarioDto)
        {
            // Adiciona o usuário
            await _con.Usuarios.AddAsync(new Usuario
            {
                Email = pUsuarioDto.Email,
                Senha = pUsuarioDto.Senha
            });

            await _con.SaveChangesAsync();
        }
    }
}