﻿using ApiCatalogo.Pagination;
using APICatalogo.Models;
using System.Collections.Generic;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
