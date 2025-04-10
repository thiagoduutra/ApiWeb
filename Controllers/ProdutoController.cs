using ApiWeb.Data;
using ApiWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<List<ProdutoModel>> BuscarProdutos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpGet]
        [Route("{pId}")]
        public ActionResult<ProdutoModel> BuscarProdutoPorId(int pId)
        {
            var produto = _context.Produtos.Find(pId);
            if (produto == null)
                return NotFound("Registro não localizado!");

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<ProdutoModel> CriarProduto(ProdutoModel pProdutoModel)
        {
            if (pProdutoModel == null)
                return BadRequest("Ocorreu um erro na solicitação!");

            _context.Produtos.Add(pProdutoModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarProdutoPorId), new { id = pProdutoModel.Id }, pProdutoModel);
        }


        [HttpPut]
        [Route("{pId}")]
        public ActionResult<ProdutoModel> EditarProduto(ProdutoModel pProdutoModel, int pId)
        {
            var produto = _context.Produtos.Find(pId);
            if (produto == null)
                return NotFound("Produto não encontrado!");

            produto.Nome = pProdutoModel.Nome;
            produto.Descricao = pProdutoModel.Descricao;
            produto.CodigoDeBarras = pProdutoModel.CodigoDeBarras;
            produto.QuantidadeEstoque = pProdutoModel.QuantidadeEstoque;
            produto.Marca = pProdutoModel.Marca;

            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [Route("{pId}")]
        public ActionResult<ProdutoModel> DeletarProduto(ProdutoModel pProdutoModel, int pId)
        {
            var produto = _context.Produtos.Find(pId);
            if (produto == null)
                return NotFound("Produto não encontrado!");

            _context.Produtos.Remove(produto);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
