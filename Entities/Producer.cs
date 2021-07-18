namespace desafio_catalogo_jogos.Entities
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Producer(string name)
        {
            Name = name;
        }

        public Producer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Producer other ? Name.Equals(other.Name) : false;
        }
    }
}