using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Models;

namespace RazorPagesApp.Controllers
{
    // [ApiController] указывает, что этот класс отвечает на HTTP-запросы и автоматически 
    // применяет правила для API (например, автоматически возвращает 400 Bad Request при неверных данных)
    [ApiController]

    // [Route("api")] задает базовый путь для всех методов в этом контроллере.
    // Это значит, что все пути внутри будут начинаться с /api/
    [Route("api/Childrens_room")]
    public class Childrens_roomApiController: ControllerBase
    {
        private readonly ApplicationContext context;

        // Внедрение зависимостей (Dependency Injection).
        // ASP.NET сам передаст сюда подключение к базе данных (ApplicationContext)
        public Childrens_roomApiController(ApplicationContext db)
        {
            context = db;
        }

        // 1. Метод для получения всех данных.
        // [HttpGet] указывает, что метод отвечает на GET-запрос.
        // Полный путь будет базовый путь ("api/Childrens_room") + путь метода ("GetAll") = /api/Childrens_room/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await context.SensorData_02
                .AsNoTracking()
                .OrderByDescending(s => s.date)
                .ToListAsync();

            // Ok() автоматически конвертирует объект data в JSON и возвращает HTTP статус 200 (OK)
            return Ok(data);
        }


        // 2. Метод для получения только последней записи с нужными полями.
        // Полный путь будет: /api/Childrens_room/GetLast
        [HttpGet("GetLast")]
        public async Task<IActionResult> GetLast()
        {
            var lastData = await context.SensorData_02
                .AsNoTracking()
                .OrderByDescending(s => s.date)
                /*.Select(s => new
                {
                    Id = s.Id,
                    temp = s.temp,
                    hum = s.hum,
                    num = s.num,
                    date = s.date
                })*/
                .FirstOrDefaultAsync();

            if (lastData == null)
            {
                // Если база пуста, возвращаем HTTP статус 404 (Not Found) с сообщением в JSON
                return NotFound(new { message = "Данные не найдены" });
            }

            return Ok(lastData);
        }


    }
}
