using desafio_catalogo_jogos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public interface IGameRepository : IBaseRepository<Game, int>
    {
        Task<Game> Get(string name, int producerId);
        Task<List<Game>> GetByProducerId(int producerId);
        Task<List<Game>> GetByProducerId(int producerId, int page, int qnt);
    }
}