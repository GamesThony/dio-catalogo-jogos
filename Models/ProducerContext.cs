using desafio_catalogo_jogos.Entities;
using Microsoft.EntityFrameworkCore;

namespace desafio_catalogo_jogos.Models
{
    public class ProducerContext : DbContext
    {
        public ProducerContext(DbContextOptions<ProducerContext> options) : base(options)
        {
        }

        public DbSet<Producer> Producers { get; set; }
    }
}