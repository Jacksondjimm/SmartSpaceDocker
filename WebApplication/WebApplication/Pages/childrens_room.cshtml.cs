using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;

using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using System.Data;

namespace RazorPagesApp.Pages
{
    public class childrens_roomModel : PageModel
    {
        // create a public property. OnGet method() set the chart configuration json in this property.
        // When the page is being loaded, OnGet method will be  invoked
        public string ChartJson { get; internal set; } // FusionCharts

        //public int count = 10;
        public async Task OnGet() // тестить на IIS: public void OnGet()
        {
            //подключение базы данных - 1начало
            SensorData_02 = context.SensorData_02.AsNoTracking().OrderBy(p => p.date).ToList();
            //подключение базы данных - 1конец

            //данные графика начало
            // create data table to store data
            DataTable ChartData = new DataTable();
            // Add columns to data table
            ChartData.Columns.Add("Temperature", typeof(System.String));
            ChartData.Columns.Add("t", typeof(System.Single));
            // Add rows to data table
            for (int i = 0; i < SensorData_02.Count; i++)
            {
                ChartData.Rows.Add($"{SensorData_02[i].date.TimeOfDay.ToString().Substring(0, 5)}, {SensorData_02[i].date.Date.ToString().Substring(0, 5)}", (SensorData_02[i].temp));
            }
            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.ColumnChart column = new Charts.ColumnChart("first_chart");
            // Set Chart's width and height
            column.Width.Pixel(700);
            column.Height.Pixel(400);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "Temperature";
            // Set chart sub title
            //column.SubCaption.Text = "2017-2018";
            // hide chart Legend
            column.Legend.Show = false;
            // set XAxis Text
            column.XAxis.Text = "Time";
            // Set YAxis title
            column.YAxis.Text = "Temperature (t)";
            // set chart theme
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;
            // set chart rendering json
            ChartJson = column.Render();
            //данные графика конец

        }
        public string PrintTime() => DateTime.Now.ToShortTimeString();
        public Sensor_02 bme280_02 { get; set; } = new();//поле для записи с датчиков в базу данных

        //подключение базы данных - 2начало
        ApplicationContext context;

        //public List<User> Users { get; private set; } = new();
        public List<Sensor_02> SensorData_02 { get; private set; } = new();

        public childrens_roomModel(ApplicationContext db)
        {
            context = db;
        }
        //подключение базы данных - 2конец
    }
}
