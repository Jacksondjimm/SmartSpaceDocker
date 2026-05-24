using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesApp.Models;
using System.Collections.Generic;
using System.Linq; // Необходимо для методов OrderByDescending, Skip и Take
using System.Threading.Tasks;

namespace RazorPagesApp.Pages
{
    public class bedroomModel : PageModel
    {
        private readonly ILogger<bedroomModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public Sensor_01 bme280_01 { get; set; } = new();
        public List<Sensor_01> SensorData_01 { get; private set; } = new();
       
        // === НАЧАЛО: Свойства для пагинации ===
        // Привязываем номер текущей страницы из URL (например, ?CurrentPage=2)
        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; } // Общее количество страниц
        public int PageSize { get; set; } = 500; // КОЛИЧЕСТВО ЗАПИСЕЙ НА СТРАНИЦЕ (можете изменить на 15, 20 и т.д.)
        // === КОНЕЦ: Свойства для пагинации ===

        public bedroomModel(ILogger<bedroomModel> logger, IHttpClientFactory httpClientFactory) // дальнейшее дублирование конструктора IndexModel не приведёт к ошибке?
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            var client = _httpClientFactory.CreateClient("BackendApi");
            bme280_01 = await client.GetFromJsonAsync<Sensor_01>("api/Bedroom/GetLast");

            // 1. Получаем все данные с API
            var allData = await client.GetFromJsonAsync<List<Sensor_01>>("api/Bedroom/GetAll")
                          ?? new List<Sensor_01>();

            // 2. Считаем общее количество страниц
            TotalPages = (int)System.Math.Ceiling(allData.Count / (double)PageSize);

            // Защита от некорректных номеров страниц в URL
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

            // 3. Сортируем, пропускаем предыдущие страницы и берем нужное количество записей
            SensorData_01 = allData
                .OrderByDescending(p => p.date)         // Сортируем от новых к старым
                .Skip((CurrentPage - 1) * PageSize)     // Пропускаем записи предыдущих страниц
                .Take(PageSize)                         // Берем записи для текущей страницы
                .ToList();

        }
    }
}
