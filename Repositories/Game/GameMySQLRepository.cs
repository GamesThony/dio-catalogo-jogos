using desafio_catalogo_jogos.Entities;
using desafio_catalogo_jogos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public class GameMySQLRepository : IGameRepository
    {
        private readonly GameContext _context;

        public GameMySQLRepository(GameContext context)
        {
            _context = context;
        }

        public async Task Delete(Game item)
        {
            _context.Games.Remove(item);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<Game>> Find(string fragment, int page, int qnt)
        {
            return await _context.Games.Where(p => p.Name.Contains(fragment)).Skip((page - 1) * qnt).Take(qnt).ToListAsync();
        }

        public async Task<Game> Get(string name, int producerId)
        {
            var items = await _context.Games.Where(p => p.Name.Equals(name) && p.ProducerId.Equals(producerId)).ToListAsync();
            return items.Count == 0 ? null : items.First();
        }

        public async Task<List<Game>> Get(int page, int qnt)
        {
            return await _context.Games.Skip((page - 1) * qnt).Take(qnt).ToListAsync();
        }

        public async Task<Game> Get(int id)
        {
            var items = await _context.Games.Where(p => p.Id.Equals(id)).ToListAsync();
            return items.Count == 0 ? null : items.First();
        }

        public async Task<List<Game>> GetByProducerId(int producerId)
        {
            return await _context.Games.Where(p => p.ProducerId.Equals(producerId)).ToListAsync();
        }

        public async Task<List<Game>> GetByProducerId(int producerId, int page, int qnt)
        {
            return await _context.Games.Where(p => p.ProducerId.Equals(producerId)).Skip((page - 1) * qnt).Take(qnt).ToListAsync();
        }

        public async Task Insert(Game item)
        {
            await _context.Games.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Game item)
        {
            _context.Games.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}