using Microsoft.AspNetCore.HttpOverrides;//для работы по http внутри сети и по https от роутера до клиента в интеренете
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Data;
using RazorPagesApp.Models;// пространство имен класса ApplicationContext
using System.Globalization; // Global Time //для передачи точки вместо запятой  //https://metanit.com/sharp/tutorial/20.4.php

var builder = WebApplication.CreateBuilder(args);

// --- НАЧАЛО ИЗМЕНЕНИЙ 1: Конфигурация заголовков (для работы по http внутри сети и по https от роутера до клиента в интеренете) ---
// Настраиваем приложение на работу за прокси (Keenetic)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    // Принимаем заголовки X-Forwarded-For (IP клиента) и X-Forwarded-Proto (протокол https)
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    // ВАЖНО: По умолчанию ASP.NET доверяет только localhost.
    // Нам нужно доверять роутеру. Самый простой способ для домашней сети — очистить списки ограничений.
    // Это говорит приложению: "Доверяй любому прокси, который передал запрос".
    // Так как сервер внутри NAT, это безопасно (доступ только через роутер).
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
// --- КОНЕЦ ИЗМЕНЕНИЙ 1 ---

// Устанавливаем культуру для всего приложения (для формата даты РФ)
//var cultureInfo = new CultureInfo("ru-RU");
var cultureInfo = new CultureInfo("en-US"); //"en-US" позволяет передавать точки вместо запятых в график библиотеки Fusion, который не понимает запятые.
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("Database"); //DefaultConnection

// добавляем контекст ApplicationContext в качестве сервиса в приложение (без миграции БД)
//builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));// 

// добавляем контекст ApplicationContext в качестве сервиса в приложение (с миграцией БД):
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(connection, npgsqlOptions =>
    {
        // Даем приложению возможность переподключиться, если БД еще "прогревается" при старте Docker
        npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(5), errorCodesToAdd: null);
    }));

// добавляем CORS (Разрешаем Frontend-у обращаться к API) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // Позже сюда можно вписать конкретные адреса (например, http://localhost:8080, http://home.smartspace...)
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// добавляем HealthChecks (Проверка работоспособности) ---
builder.Services.AddHealthChecks()
    .AddNpgSql(connection, name: "database_check"); // Проверяет, жива ли БД

// добавляем в приложение сервисы Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new SafeFloatConverter());
    });

var app = builder.Build();

// --- НАЧАЛО ИЗМЕНЕНИЙ 2: Подключение Middleware  (для работы по http внутри сети и по https от роутера до клиента в интеренете) ---
// Это должно быть ПЕРВЫМ middleware в конвейере или как можно выше
app.UseForwardedHeaders();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Если ты хочешь, чтобы при заходе по http внутри сети тебя все равно перекидывало на https (не обязательно, но полезно)
app.UseHttpsRedirection();
// --- КОНЕЦ ИЗМЕНЕНИЙ 2 ---

// Применяем политику CORS
app.UseCors("AllowFrontend");

// Мапим эндпоинт для проверки здоровья
app.MapHealthChecks("/health");

// конструкция, позволяющая внедрить Postgre и DbInitializer:
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationContext>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); // строчка для устранения исключения по timestamp при переходе на postgree
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true); // строчка для устранения исключения по timestamp при переходе на postgree
        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated(); //(без миграции БД)
        context.Database.Migrate();  // После применения миграций можно наполнять базу стартовыми данными
        DbInitializer.Initialize(context);
    }

    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ошибка при подготовке базы данных.");
    }


}

// добавляем поддержку маршрутизации для Razor Pages
app.MapRazorPages();
app.MapControllers();

app.Run();


