using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Services;
using Pedidos.Domain.DTos;
using Pedidos.Domain.Model.UsuariosAggregate;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Controllers
{
    [ApiController]
    [Route("api/v1/Auth")]
    public class AuthController(IUsuarioRepository usuarioRepository) : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));

        [HttpPost]
        [Route("Auth")]
        [SwaggerOperation(Summary = "Retorna token baseado no usuário.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Usuário não encontrado.")]
        public IActionResult Auth([FromBody][Required] UsuarioDto authUser)
        {
            Usuario? usuario = _usuarioRepository.GetUsuario(authUser.Email, authUser.Senha);

            if (!string.IsNullOrEmpty(authUser.Email))
            {
                if (usuario is not null && usuario.Email == authUser.Email && usuario.Senha == authUser.Senha)
                {
                    var token = TokenService.GenerateToken(new Usuario(authUser.Email));

                    return Ok(token);
                }
            }

            return BadRequest("Usuario não encontrado");
        }

        [HttpPost]
        [Route("AddUsuario")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> AddAsync([FromBody] UsuarioDto authUser)
        {
            if (!string.IsNullOrEmpty(authUser.Email))
            {
                if (authUser is not null)
                {
                    if (string.IsNullOrEmpty(authUser.Email))
                    {
                        return BadRequest("Email vazio ou inválido.");
                    }

                    if (string.IsNullOrEmpty(authUser.Senha))
                    {
                        return BadRequest("Senha vazia ou inválida.");
                    }

                    await _usuarioRepository.PostUsuario(authUser);
                    return Ok("OK");
                }
            }

            return BadRequest("Usuario inválido.");
        }
    }
}