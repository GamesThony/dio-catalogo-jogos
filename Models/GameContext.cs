using desafio_catalogo_jogos.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafio_catalogo_jogos.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
    }
}