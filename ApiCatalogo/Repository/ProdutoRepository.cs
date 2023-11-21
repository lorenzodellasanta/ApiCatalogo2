using ApiCatalogo.Pagination;
using APICatalogo.Context;
using APICatalogo.Models;
using System.Collections.Generic;
using System.Linq;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtoParameters)
        {
            return PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.Nome),
                produtoParameters.PageNumber,
                produtoParameters.PageSize);
        }
        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
