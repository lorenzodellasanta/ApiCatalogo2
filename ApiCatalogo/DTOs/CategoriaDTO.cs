using APICatalogo.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class CategoriaDTO
    {
        public CategoriaDTO()
        {
            Produtos = new Collection<ProdutoDTO>();
        }
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        //[BindNever]
        public string? ImagemUrl { get; set; }

        public ICollection<ProdutoDTO>? Produtos { get; set; }
    }
}
