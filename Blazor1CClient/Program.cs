using Blazor1CClient.Components;
using Blazor1CClient.Configurations;
using Blazor1CClient.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Добавление Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Добавление конфигурации API с использованием Options
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Регистрация HttpClient с использованием ApiSettings из IOptions
builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl) };
});

// Регистрация DataService
builder.Services.AddScoped<IStudentService, StudentService>();  

// Добавление CORS политики
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Разрешить запросы с любого источника
              .AllowAnyMethod()  // Разрешить любые HTTP-методы
              .AllowAnyHeader(); // Разрешить любые заголовки
    });
});

var app = builder.Build();

// Настройка HTTP-конвейера
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");  // Обработка ошибок
    app.UseHsts();  // Включение HSTS
}

app.UseHttpsRedirection();  // Перенаправление на HTTPS
app.UseStaticFiles();  // Раздача статических файлов
app.UseAntiforgery();  // Защита от CSRF атак

// Настройка маршрутизации Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();  // Запуск приложения