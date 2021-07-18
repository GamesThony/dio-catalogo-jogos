using desafio_catalogo_jogos.Entities;
using desafio_catalogo_jogos.Exceptions;
using desafio_catalogo_jogos.Models;
using desafio_catalogo_jogos.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private readonly IGameRepository _gameRepository;

        public ProducerService(IProducerRepository producerRepository, IGameRepository gameRepository)
        {
            _producerRepository = producerRepository;
            _gameRepository = gameRepository;
        }

        public async Task Delete(int id)
        {
            var producer = await _producerRepository.Get(id);
            if (producer == null) throw new ProducerNotFoundException();

            var games = await _gameRepository.GetByProducerId(id);
            if (games.Count > 0) throw new ExistingGamesWithProducerException();

            await _producerRepository.Delete(producer);
        }

        public void Dispose()
        {
            _producerRepository?.Dispose();
            _gameRepository?.Dispose();
        }

        public async Task<List<ProducerViewModel>> Find(string fragment, int page, int qnt)
        {
            var producers = await _producerRepository.Find(fragment, page, qnt);

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public async Task<ProducerViewModel> Get(string name)
        {
            var producer = await _producerRepository.Get(name);
            if (producer == null) throw new ProducerNotFoundException();

            return new ProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
        }

        public async Task<List<ProducerViewModel>> Get(int pag, int qnt)
        {
            var producers = await _producerRepository.Get(pag, qnt);

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public async Task<ProducerViewModel> Get(int id)
        {
            var producer = await _producerRepository.Get(id);
            if (producer == null) throw new ProducerNotFoundException();

            return new ProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
        }

        public async Task<ProducerViewModel> Insert(ProducerInputModel input)
        {
            var exists = await _producerRepository.Get(input.Name) != null;
            if (exists) throw new ProducerAlreadyExistsException();

            var producer = new Producer(input.Name);
            await _producerRepository.Insert(producer);

            return new ProducerViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
        }

        public async Task Update(int id, ProducerInputModel input)
        {
            var entity = await _producerRepository.Get(id);
            if (entity == null) throw new ProducerNotFoundException();

            var exists = await _producerRepository.Get(input.Name) != null;
            if (exists) throw new ProducerAlreadyExistsException();

            entity.Name = input.Name;
            await _producerRepository.Update(entity);
        }
    }
}