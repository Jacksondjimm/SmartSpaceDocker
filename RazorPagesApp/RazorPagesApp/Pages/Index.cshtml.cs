using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace RazorPagesApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Sensor_01 bme280_01 { get; set; } = new();
        public Sensor_02 bme280_02 { get; set; } = new();
        public Sensor_03 bme280_03 { get; set; } = new();
        public Sensor_04 bme280_04 { get; set; } = new();
        public Sensor_05 bme280_05 { get; set; } = new();
        public List<Sensor_01> SensorData_01 { get; private set; } = new();
        public List<Sensor_02> SensorData_02 { get; private set; } = new();
        public List<Sensor_03> SensorData_03 { get; private set; } = new();
        public List<Sensor_04> SensorData_04 { get; private set; } = new();
        public List<Sensor_05> SensorData_05 { get; private set; } = new();
        /*
        public IndexModel(ILogger<IndexModel> logger) // дальнейшее дублирование конструктора IndexModel не приведёт к ошибке?
        {
            _logger = logger;
        }
        
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        */
        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory) // дальнейшее дублирование конструктора IndexModel не приведёт к ошибке?
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");
            //var response = await client.GetAsync("http://localhost:5223/api/GetLastData");
            //var response = await client.GetAsync("https://home.smartspace.netcraze.link/");

            bme280_01 = await client.GetFromJsonAsync<Sensor_01>("api/Bedroom/GetLast");
            bme280_02 = await client.GetFromJsonAsync<Sensor_02>("api/Childrens_room/GetLast");
            bme280_03 = await client.GetFromJsonAsync<Sensor_03>("api/Hall/GetLast");
            bme280_04 = await client.GetFromJsonAsync<Sensor_04>("api/Hallway/GetLast");
            bme280_05 = await client.GetFromJsonAsync<Sensor_05>("api/Kitchen/GetLast");
            /*if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
            }*/
        }
    }
}
