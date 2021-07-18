using System;

namespace desafio_catalogo_jogos.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException() : base("Jogo não encontrado!")
        { }
    }
}