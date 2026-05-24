using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using System.Data;

namespace RazorPagesApp.Pages
{
    public class kitchenModel : PageModel
    {
        public async Task OnGet()
        {
            //подключение базы данных - 1начало
            SensorData_05 = context.SensorData_05.AsNoTracking().ToList();
            //подключение базы данных - 1конец
        }
        public string PrintTime() => DateTime.Now.ToShortTimeString();
        public Sensor_05 bme280_05 { get; set; } = new();//поле для записи с датчиков в базу данных

        //подключение базы данных - 2начало
        ApplicationContext context;

        public List<Sensor_05> SensorData_05 { get; private set; } = new();

        public kitchenModel(ApplicationContext db)
        {
            context = db;
        }
        //подключение базы данных - 2конец
    }
}
