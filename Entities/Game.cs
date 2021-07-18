namespace desafio_catalogo_jogos.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Game(int producerId, string name, double price)
        {
            ProducerId = producerId;
            Name = name;
            Price = price;
        }

        public Game(int id, int producerId, string name, double price)
        {
            Id = id;
            ProducerId = producerId;
            Name = name;
            Price = price;
        }

        public override int GetHashCode()
        {
            return ProducerId.GetHashCode() + Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Game other ? ProducerId.Equals(other.ProducerId) && Name.Equals(other.Name) : false;
        }
    }
}