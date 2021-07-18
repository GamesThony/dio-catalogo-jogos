using System.ComponentModel.DataAnnotations;

namespace desafio_catalogo_jogos.Models
{
    public class ProducerInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do produtor deve ter entre 3 e 100 caracteres!")]
        public string Name { get; set; }
    }
}