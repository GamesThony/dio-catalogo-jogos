using desafio_catalogo_jogos.Entities;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public interface IProducerRepository : IBaseRepository<Producer, int>
    {
        Task<Producer> Get(string name);
    }
}