using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pedidos.Domain.DTos;
using Pedidos.Domain.Model.ProdutoAggregate;
using Swashbuckle.AspNetCore.Annotations;

namespace Pedidos.Controllers
{
    [ApiController]
    [Route("api/v1/Produtos")]
    public class ProdutoController(IProdutoRepository produtoRepository) : Controller
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));

        [Authorize]
        [HttpPost]
        [Route("AddOrUpdateProduto")]
        [SwaggerOperation(Summary = "Insere produto.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> AddOrUpdateProduto(ProdutoDTo pProduto)
        {
            if (pProduto is not null)
            {
                object oRet = await _produtoRepository.AddOrUpdateProduto(pProduto);
                return Ok(oRet);
            }

            return BadRequest("Algo errado na inserção do pedido.");
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllProdutos")]
        [SwaggerOperation(Summary = "Retorna lista de produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> GetAllProdutos([FromQuery] int pPagina = 1, [FromQuery] int pItensPorPagina = 10)
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

            object oRet = await _produtoRepository.GetAllProdutos(pPagina, pItensPorPagina);
            return Ok(oRet);
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteProduto{pId}")]
        [SwaggerOperation(Summary = "Retorna lista de produtos.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Ok.")]
        public async Task<IActionResult> DeleteProduto([FromRoute] int pId)
        {
            object oRet = await _produtoRepository.DeleteProduto(pId);
            return Ok(oRet);
        }
    }
}