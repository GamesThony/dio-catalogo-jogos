using System;

namespace desafio_catalogo_jogos.Exceptions
{
    public class ProducerAlreadyExistsException : Exception
    {
        public ProducerAlreadyExistsException() : base("Já foi cadastrado um produtor com esse nome!")
        { }
    }
}