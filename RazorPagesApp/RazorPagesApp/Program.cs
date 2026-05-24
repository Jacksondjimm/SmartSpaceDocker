using Microsoft.AspNetCore.HttpOverrides;
using System.Globalization; // Global Time

var builder = WebApplication.CreateBuilder(args);

// 1. Настройка для Reverse Proxy (Keenetic) - начало
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});
// Настройка для Reverse Proxy (Keenetic) - конец

// Устанавливаем культуру для всего приложения (для формата даты РФ)
//var cultureInfo = new CultureInfo("ru-RU");
var cultureInfo = new CultureInfo("en-US"); //"en-US" позволяет передавать точки вместо запятых в график библиотеки Fusion, который не понимает запятые.
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddHttpClient();

// 2. РЕГИСТРАЦИЯ ИМЕНОВАННОГО HTTP КЛИЕНТА! начало
// Берем URL из appsettings.json (или из Docker Environment variables)
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");

builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
    // Можно сразу задать таймауты и заголовки по умолчанию, если нужно
});

// РЕГИСТРАЦИЯ ИМЕНОВАННОГО HTTP КЛИЕНТА! конец

var app = builder.Build();

app.UseForwardedHeaders(); // Включаем прокси


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
