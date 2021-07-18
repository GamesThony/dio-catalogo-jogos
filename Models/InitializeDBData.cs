using desafio_catalogo_jogos.Entities;
using System.Linq;

namespace desafio_catalogo_jogos.Models
{
    public static class InitializeDBData
    {
        public static void Initialize(GameContext gameContext, ProducerContext producerContext)
        {
            gameContext.Database.EnsureCreated();
            producerContext.Database.EnsureCreated();

            bool anyGame = gameContext.Games.Any();
            bool anyProducer = producerContext.Producers.Any();

            if (anyGame && anyProducer) return;

            if (!anyProducer)
            {
                producerContext.Producers.Add(new Producer(1, "Rockstar Games"));
                producerContext.Producers.Add(new Producer(2, "Ubisoft Reflections"));
                producerContext.Producers.Add(new Producer(3, "Behavior Interactive"));
                producerContext.Producers.Add(new Producer(4, "Frictional Games"));
                producerContext.SaveChanges();
            }

            if (!anyGame)
            {
                gameContext.Games.Add(new Game(1, 1, "Grand Theft Auto IV", 39.99));
                gameContext.Games.Add(new Game(2, 2, "Driver: San Francisco", 29.99));
                gameContext.Games.Add(new Game(3, 3, "Dead by Daylight", 26.99));
                gameContext.Games.Add(new Game(4, 1, "Grand Theft Auto: San Andreas", 15.99));
                gameContext.Games.Add(new Game(5, 4, "SOMA", 45.99));
                gameContext.Games.Add(new Game(6, 1, "Red Dead Redemption 2", 205.99));
                gameContext.SaveChanges();
            }
        }
    }
}