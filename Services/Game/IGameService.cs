using desafio_catalogo_jogos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Services
{
    public interface IGameService : IBaseService<GameViewModel, GameInputModel, int>
    {
        Task<GameViewModel> Get(string name, int producerId);
        Task<List<GameViewModel>> GetByProducerId(int producerId);
        Task<List<GameViewModel>> GetByProducerId(int producerId, int page, int qnt);
        Task Update(int id, double price);
        Task DeleteAllWithProducerId(int producerId);
    }
}