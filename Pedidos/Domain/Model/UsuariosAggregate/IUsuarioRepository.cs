using Pedidos.Domain.DTos;

namespace Pedidos.Domain.Model.UsuariosAggregate
{
    public interface IUsuarioRepository
    {
        Usuario? GetUsuario(string pEmail, string pSenha);
        Task PostUsuario(UsuarioDto pUsuarioDto);
    }
}