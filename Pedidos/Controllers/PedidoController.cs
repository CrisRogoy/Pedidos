using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Domain.DTos;
using Pedidos.Domain.Enums;
using Pedidos.Domain.Model.PedidoAggregate;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Pedidos.Controllers
{
    [ApiController]
    [Route("api/v1/Pedidos")]
    public class PedidoController(IPedidoRepository pedidoRepository) : Controller
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));

        [Authorize]
        [HttpPost]
        [Route("AddPedido")]
        [SwaggerOperation(Summary = "Insere pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> AddPedido(PedidoDTo pPedido)
        {
            if (pPedido is not null)
            {
                object oRet = await _pedidoRepository.AddPedido(pPedido);
                return Ok(oRet);
            }

            return BadRequest("Algo errado na inserção do pedido.");
        }

        [Authorize]
        [HttpPost]
        [Route("InsereItemPedido")]
        [SwaggerOperation(Summary = "Insere item no pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> InsereItemPedido(ItensPedidoDTo pItemPedido)
        {
            if (pItemPedido is not null)
            {
                object oRet = await _pedidoRepository.AddItensPedido(pItemPedido);
                return Ok(oRet);
            }

            return BadRequest("Algo errado na inserção do item no pedido.");
        }

        [Authorize]
        [HttpDelete]
        [Route("{pIdPedido}/Itens/{pIdItemPedido}")]
        [SwaggerOperation(Summary = "Deleta item específico de um pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Item deletado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido ou item não encontrado.")]
        public async Task<IActionResult> DeleteItemPedido([FromRoute][Required] int pIdPedido, [FromRoute][Required] int pIdItemPedido)
        {
            object oRet = await _pedidoRepository.DeleteItemPedido(pIdPedido, pIdItemPedido);

            if (oRet == null)
            {
                return NotFound(new
                {
                    Msg = "Pedido ou item não encontrado."
                });
            }

            return Ok(oRet);
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateStatusPedido")]
        [SwaggerOperation(Summary = "Atualiza status do pedido.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> UpdateStatusPedido([Required] int pIdPedido, [Required] StatusPedido pStatus)
        {
            object oRet = await _pedidoRepository.UpdateStatusPedido(pIdPedido, pStatus);
            return Ok(oRet);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPedidosSemItens")]
        [SwaggerOperation(Summary = "Retorna lista de pedidos paginada.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> GetPedidosSemItens([FromQuery] StatusPedido? pStatus = null, [FromQuery] int pPagina = 1, [FromQuery] int pItensPorPagina = 10)
        {
            if (pPagina < 1)
            {
                pPagina = 1;
            }

            if (pItensPorPagina < 1)
            {
                pItensPorPagina = 10;
            }

            if (pItensPorPagina > 100)
            {
                pItensPorPagina = 100; // Limite máximo
            }

            object oRet = await _pedidoRepository.GetPedidosSemItens(pStatus, pPagina, pItensPorPagina);
            return Ok(oRet);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPedidoComItens/{pId}")]
        [SwaggerOperation(Summary = "Retorna lista de pedidos com itens.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> GetPedidoComItens([FromRoute][Required] int pId)
        {
            object oRet = await _pedidoRepository.GetPedidoByIdComItens(pId);
            return Ok(oRet);
        }
    }
}