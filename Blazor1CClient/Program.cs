using Blazor1CClient.Components;
using Blazor1CClient.Configurations;
using Blazor1CClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавление Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Добавление конфигурации API
builder.Services.AddSingleton(builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>());

// Регистрация HttpClient с базовым адресом из конфигурации
builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<ApiSettings>();
    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl) };
});

// Регистрация DataService
builder.Services.AddScoped<DataService>();

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

// Регистрация сервисов для MVC или Web API (если необходимо)
builder.Services.AddControllers();  // Для API
builder.Services.AddRazorComponents();  // Если это Blazor Server

// Регистрация локализатора для компонентов Syncfusion
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


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
