using System;

namespace desafio_catalogo_jogos.Exceptions
{
    public class GameAlreadyExistsException : Exception
    {
        public GameAlreadyExistsException() : base("Já foi cadastrado um jogo com esse nome e produtor!") 
        { }
    }
}