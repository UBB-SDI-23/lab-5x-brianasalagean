using System.ComponentModel.DataAnnotations.Schema;

namespace lab5.Models
{
    public class SuperHero
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Place { get; set; } = String.Empty;
        public int ManagerId { get; set; }
    }
}
