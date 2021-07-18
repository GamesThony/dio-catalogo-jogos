using desafio_catalogo_jogos.Entities;
using desafio_catalogo_jogos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public class ProducerMySQLRepository : IProducerRepository
    {
        private readonly ProducerContext _context;

        public ProducerMySQLRepository(ProducerContext context)
        {
            _context = context;
        }

        public async Task Delete(Producer item)
        {
            _context.Producers.Remove(item);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<Producer>> Find(string fragment, int page, int qnt)
        {
            return await _context.Producers.Where(p => p.Name.Contains(fragment)).Skip((page - 1) * qnt).Take(qnt).ToListAsync();
        }

        public async Task<Producer> Get(string name)
        {
            var items = await _context.Producers.Where(p => p.Name.Equals(name)).ToListAsync();
            return items.Count == 0 ? null : items.First();
        }

        public async Task<List<Producer>> Get(int page, int qnt)
        {
            return await _context.Producers.Skip((page - 1) * qnt).Take(qnt).ToListAsync();
        }

        public async Task<Producer> Get(int id)
        {
            var items = await _context.Producers.Where(p => p.Id == id).ToListAsync();
            return items.Count == 0 ? null : items.First();
        }

        public async Task Insert(Producer item)
        {
            await _context.Producers.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Producer item)
        {
            _context.Producers.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}