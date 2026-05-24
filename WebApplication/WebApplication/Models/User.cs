namespace RazorPagesApp.Models 
{
    public class User 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public DateTimeOffset date { get; set; }
    }
}
