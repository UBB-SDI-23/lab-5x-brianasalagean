namespace lab5.Models
{
    public class SuperPower
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public bool HasAntidote { get; set; }
        public bool IsControlled { get; set;}
    }
}
