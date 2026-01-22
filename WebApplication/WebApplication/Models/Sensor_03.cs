namespace RazorPagesApp.Models
{
    public class Sensor_03
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }

        public float temp { get; set; }
        public float hum { get; set; }
        public float num { get; set; }
        public DateTimeOffset date { get; set; }
    }
}
