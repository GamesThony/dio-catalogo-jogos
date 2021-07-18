using System;

namespace desafio_catalogo_jogos.Exceptions
{
    public class ProducerNotFoundException : Exception
    {
        public ProducerNotFoundException() : base("Produtor não encontrado!")
        { }
    }
}