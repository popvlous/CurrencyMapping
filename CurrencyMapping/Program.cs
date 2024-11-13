using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CurrencyMapping.Data;
using LoggerLocalFile;
using Microsoft.OpenApi.Models;
using CurrencyMapping.Middlewares;
using CurrencyMapping.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CurrencyMappingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurrencyMappingContext") ?? throw new InvalidOperationException("Connection string 'CurrencyMappingContext' not found.")));

// Add services to the container.
// 使用DI注入各種服務
builder.Logging.AddLocalFileLogger(options => { options.SaveDays = 7; });

builder.Services.AddHttpClient<BitcoinPriceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddDataProtection();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    // API 服務簡介
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "幣別中文化 API",
        Description = "幣別中文化 API"
    });
});

var app = builder.Build();
// 注入 Middleware 中介程序，收集 HTTP Request 資訊
app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
