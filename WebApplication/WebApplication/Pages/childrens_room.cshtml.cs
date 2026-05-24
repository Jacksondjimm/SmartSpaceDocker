using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using System.Data;

namespace RazorPagesApp.Pages
{
    public class childrens_roomModel : PageModel
    {

        public async Task OnGet() 
        {
            //подключение базы данных - 1начало
            SensorData_02 = context.SensorData_02.AsNoTracking().ToList();
            //подключение базы данных - 1конец

        }
        public string PrintTime() => DateTime.Now.ToShortTimeString();
        public Sensor_02 bme280_02 { get; set; } = new();//поле для записи с датчиков в базу данных

        //подключение базы данных - 2начало
        ApplicationContext context;

        public List<Sensor_02> SensorData_02 { get; private set; } = new();

        public childrens_roomModel(ApplicationContext db)
        {
            context = db;
        }
        //подключение базы данных - 2конец
    }
}
