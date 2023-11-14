using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }

        //[HttpGet("{valor:alpha:length(3)}")]
        //public ActionResult<Produto> Get2()
        //{
        //    var produto = _uof.Produtos.FirstOrDefault();
        //    if (produto is null)
        //    {
        //        return NotFound();
        //    }
        //    return produto;
        //}

        [HttpGet("menorPreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDto;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParameters).ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDto;
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produtos = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produtos == null)
            {
                return NotFound();
            }

            var produtosDto = _mapper.Map<ProdutoDTO>(produtos);

            return produtosDto;
        }


        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {

            var produto = _mapper.Map<Produto>(produtoDto);
            if (produto is null)
                return BadRequest();

            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO> (produto); 

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produtoDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDto);

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult <ProdutoDTO>Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            //var produto = _uof.Produtos.Find(id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return produtoDto;
        }
    }
}
