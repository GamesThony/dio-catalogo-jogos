using desafio_catalogo_jogos.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Repositories
{
    public class ProducerRepository : IProducerRepository
    {
        private static Dictionary<int, Producer> producers = new Dictionary<int, Producer>()
        {
            { 1, new Producer(1, "Rockstar Games") },
            { 2, new Producer(2, "Ubisoft Reflections") },
            { 3, new Producer(3, "Behavior Interactive") },
            { 4, new Producer(4, "Frictional Games") }
        };

        public Task Delete(Producer producer)
        {
            producers.Remove(producer.Id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // fechar conexão
        }

        public Task<List<Producer>> Find(string fragment, int page, int qnt)
        {
            return Task.FromResult(producers.Values.Where(p => p.Name.Contains(fragment)).Skip((page - 1) * qnt).Take(qnt).ToList());
        }

        public Task<Producer> Get(string name)
        {
            var items = producers.Values.Where(p => p.Name.Equals(name));
            return Task.FromResult(items.Count() == 0 ? null : items.First());
        }

        public Task<List<Producer>> Get(int page, int qnt)
        {
            return Task.FromResult(producers.Values.Skip((page - 1) * qnt).Take(qnt).ToList());
        }

        public Task<Producer> Get(int id)
        {
            var items = producers.Values.Where(p => p.Id == id);
            return Task.FromResult(items.Count() == 0 ? null : items.First());
        }

        public Task Insert(Producer item)
        {
            producers.Add(item.Id, item);
            return Task.CompletedTask;
        }

        public Task Update(Producer item)
        {
            producers[item.Id] = item;
            return Task.CompletedTask;
        }
    }
}