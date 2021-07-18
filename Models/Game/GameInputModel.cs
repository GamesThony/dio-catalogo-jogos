using System.ComponentModel.DataAnnotations;

namespace desafio_catalogo_jogos.Models
{
    public class GameInputModel
    {
        [Required]
        public int ProducerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo precisa ter entre 3 e 100 caracteres!")]
        public string Name { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do preço precisa ser no mínimo 1 e no máximo 1000 reais")]
        public double Price { get; set; }
    }
}