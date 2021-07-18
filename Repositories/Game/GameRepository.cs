using desafio_catalogo_jogos.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<int, Game> games = new Dictionary<int, Game>()
        {
            { 1, new Game(1, 1, "Grand Theft Auto IV", 39.99) },
            { 2, new Game(2, 2, "Driver: San Francisco", 35.99) },
            { 3, new Game(3, 3, "Dead by Daylight", 29.99) },
            { 4, new Game(4, 1, "Grand Theft Auto: San Andreas", 15.99) },
            { 5, new Game(5, 1, "Red Dead Redemption 2", 159.99) },
            { 6, new Game(6, 4, "SOMA", 153.65) }
        };

        public Task Delete(Game game)
        {
            games.Remove(game.Id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // fechar conexão
        }

        public Task<List<Game>> Find(string fragment, int page, int qnt)
        {
            return Task.FromResult(games.Values.Where(p => p.Name.Contains(fragment)).Skip((page - 1) * qnt).Take(qnt).ToList());
        }

        public Task<Game> Get(string name, int producerId)
        {
            var items = games.Values.Where(p => p.Name.Equals(name) && p.ProducerId.Equals(producerId));
            return Task.FromResult(items.Count() == 0 ? null : items.First());
        }

        public Task<List<Game>> Get(int page, int qnt)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * qnt).Take(qnt).ToList());
        }

        public Task<Game> Get(int id)
        {
            var items = games.Values.Where(p => p.Id == id);
            return Task.FromResult(items.Count() == 0 ? null : items.First());
        }

        public Task<List<Game>> GetByProducerId(int producerId)
        {
            return Task.FromResult(games.Values.Where(p => p.ProducerId.Equals(producerId)).ToList());
        }

        public Task<List<Game>> GetByProducerId(int producerId, int page, int qnt)
        {
            return Task.FromResult(games.Values.Where(p => p.ProducerId.Equals(producerId)).Skip((page - 1) * qnt).Take(qnt).ToList());
        }

        public Task Insert(Game item)
        {
            games.Add(item.Id, item);
            return Task.CompletedTask;
        }

        public Task Update(Game item)
        {
            games[item.Id] = item;
            return Task.CompletedTask;
        }
    }
}