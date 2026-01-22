using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;
using RazorPagesApp.Models;// пространство имен класса ApplicationContext
using System.Globalization; // Global Time

var builder = WebApplication.CreateBuilder(args);

// Устанавливаем культуру для всего приложения
//var cultureInfo = new CultureInfo("ru-RU");
var cultureInfo = new CultureInfo("en-US"); //"en-US" позволяет передавать точки вместо запятых в график библиотеки Fusion, который не понимает запятые.
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("Database"); //DefaultConnection

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

// добавляем в приложение сервисы Razor Pages
builder.Services.AddRazorPages();
var app = builder.Build();

// конструкция, позволяющая внедрить Postgre и DbInitializer:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationContext>();
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // строчка для устранения исключения по timestamp при переходе на postgree
    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true); // строчка для устранения исключения по timestamp при переходе на postgree
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);

}

// добавляем поддержку маршрутизации для Razor Pages
app.MapRazorPages();

app.Run();
