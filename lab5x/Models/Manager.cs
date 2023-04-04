namespace lab5.Models
{
    public class Manager
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public int Age { get; set; }
        public int YearsExp { get; set; }
    }
}
