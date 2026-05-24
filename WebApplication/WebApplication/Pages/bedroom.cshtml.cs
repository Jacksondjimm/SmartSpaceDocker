using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.Serialization;
using System.Globalization; // для передачи точки вместо запятой  //https://metanit.com/sharp/tutorial/20.4.php
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Data;

namespace RazorPagesApp.Pages
{
    public class bedroomModel: PageModel
    {
        public async Task OnGet() 
        {
            //подключение базы данных - 1начало
            SensorData_01 = context.SensorData_01.AsNoTracking().ToList();
            //подключение базы данных - 1конец

        }
        public string PrintTime() => DateTime.Now.ToShortTimeString();
        public Sensor_01 bme280_01 { get; set; } = new();//поле для записи с датчиков в базу данных

        //подключение базы данных - 2начало
        ApplicationContext context;

        public List<Sensor_01> SensorData_01 { get; private set; } = new();

        public bedroomModel(ApplicationContext db)
        {
            context = db;
        }
        //подключение базы данных - 2конец
    }
}
