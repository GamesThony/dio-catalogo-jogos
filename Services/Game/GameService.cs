using desafio_catalogo_jogos.Entities;
using desafio_catalogo_jogos.Exceptions;
using desafio_catalogo_jogos.Models;
using desafio_catalogo_jogos.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_catalogo_jogos.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IProducerRepository _producerRepository;

        public GameService(IGameRepository gameRepository, IProducerRepository producerRepository)
        {
            _gameRepository = gameRepository;
            _producerRepository = producerRepository;
        }

        public async Task Delete(int id)
        {
            var game = await _gameRepository.Get(id);
            if (game == null) throw new GameNotFoundException();
            await _gameRepository.Delete(game);
        }

        public async Task DeleteAllWithProducerId(int producerId)
        {
            var games = await _gameRepository.GetByProducerId(producerId);
            foreach (var game in games) await Delete(game.Id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
            _producerRepository?.Dispose();
        }

        public async Task<List<GameViewModel>> Find(string fragment, int page, int qnt)
        {
            var games = await _gameRepository.Find(fragment, page, qnt);

            return games.Select(p => new GameViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ProducerId = p.ProducerId,
                Price = p.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(string name, int producerId)
        {
            var game = await _gameRepository.Get(name, producerId);
            if (game == null) throw new GameNotFoundException();
            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                ProducerId = game.ProducerId,
                Price = game.Price
            };
        }

        public async Task<List<GameViewModel>> Get(int pag, int qnt)
        {
            var games = await _gameRepository.Get(pag, qnt);
            return games.Select(p => new GameViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ProducerId = p.ProducerId,
                Price = p.Price
            }).ToList();
        }

        public async Task<GameViewModel> Get(int id)
        {
            var game = await _gameRepository.Get(id);
            if (game == null) throw new GameNotFoundException();
            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                ProducerId = game.ProducerId,
                Price = game.Price
            };
        }

        public async Task<List<GameViewModel>> GetByProducerId(int producerId)
        {
            var producerExists = await _producerRepository.Get(producerId) != null;
            if (!producerExists) throw new ProducerNotFoundException();

            var games = await _gameRepository.GetByProducerId(producerId);
            return games.Select(p => new GameViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ProducerId = p.ProducerId,
                Price = p.Price
            }).ToList();
        }

        public async Task<List<GameViewModel>> GetByProducerId(int producerId, int page, int qnt)
        {
            var games = await _gameRepository.GetByProducerId(producerId, page, qnt);
            return games.Select(p => new GameViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ProducerId = p.ProducerId,
                Price = p.Price
            }).ToList();
        }

        public async Task<GameViewModel> Insert(GameInputModel input)
        {
            var existsProducer = await _producerRepository.Get(input.ProducerId) != null;
            if (!existsProducer) throw new ProducerNotFoundException();

            var exists = await _gameRepository.Get(input.Name, input.ProducerId) != null;
            if (exists) throw new GameAlreadyExistsException();

            var game = new Game(input.ProducerId, input.Name, input.Price);
            await _gameRepository.Insert(game);

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                ProducerId = game.ProducerId,
                Price = game.Price
            };
        }

        public async Task Update(int id, double price)
        {
            var entity = await _gameRepository.Get(id);
            if (entity == null) throw new GameNotFoundException();
            
            entity.Price = price;
            await _gameRepository.Update(entity);
        }

        public async Task Update(int id, GameInputModel input)
        {
            var existsProducer = await _producerRepository.Get(input.ProducerId) != null;
            if (!existsProducer) throw new ProducerNotFoundException();

            var entity = await _gameRepository.Get(id);
            if (entity == null) throw new GameNotFoundException();

            var exists = await _gameRepository.Get(input.Name, input.ProducerId) != null;
            if (exists) throw new GameAlreadyExistsException();

            entity.ProducerId = input.ProducerId;
            entity.Name = input.Name;
            entity.Price = input.Price;
            await _gameRepository.Update(entity);
        }
    }
}