using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;

namespace RazorPagesApp.Models
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Sensor_01> SensorData_01 { get; set; } = null!;
        public DbSet<Sensor_02> SensorData_02 { get; set; } = null!;
        public DbSet<Sensor_03> SensorData_03 { get; set; } = null!;
        public DbSet<Sensor_04> SensorData_04 { get; set; } = null!;
        public DbSet<Sensor_05> SensorData_05 { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!; // for test only
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
           // Database.EnsureCreated();// создаем базу данных при первом обращении
        }
    }
}
