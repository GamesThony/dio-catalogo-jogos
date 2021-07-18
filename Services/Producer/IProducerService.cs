using desafio_catalogo_jogos.Models;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Services
{
    public interface IProducerService : IBaseService<ProducerViewModel, ProducerInputModel, int>
    {
        Task<ProducerViewModel> Get(string name);
    }
}