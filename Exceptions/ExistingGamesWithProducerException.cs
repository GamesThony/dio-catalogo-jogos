using System;

namespace desafio_catalogo_jogos.Exceptions
{
    public class ExistingGamesWithProducerException : Exception
    {
        public ExistingGamesWithProducerException() : base("Ainda existe jogos cadastrados com esse produtor!")
        { }
    }
}