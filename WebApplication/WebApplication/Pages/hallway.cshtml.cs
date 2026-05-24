using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using System.Data;

namespace RazorPagesApp.Pages
{
    public class hallwayModel : PageModel
    {
        public async Task OnGet() 
        {
            //подключение базы данных - 1начало
            SensorData_04 = context.SensorData_04.AsNoTracking().ToList();
            //подключение базы данных - 1конец

        }
        public string PrintTime() => DateTime.Now.ToShortTimeString();
        public Sensor_04 bme280_04 { get; set; } = new();//поле для записи с датчиков в базу данных

        //подключение базы данных - 2начало
        ApplicationContext context;

        public List<Sensor_04> SensorData_04 { get; private set; } = new();

        public hallwayModel(ApplicationContext db)
        {
            context = db;
        }
        //подключение базы данных - 2конец
    }
}
