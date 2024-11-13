using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CurrencyMapping.Data;
using LoggerLocalFile;
using Microsoft.OpenApi.Models;
using CurrencyMapping.Middlewares;
using CurrencyMapping.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CurrencyMappingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CurrencyMappingContext") ?? throw new InvalidOperationException("Connection string 'CurrencyMappingContext' not found.")));

// Add services to the container.
// �ϥ�DI�`�J�U�تA��
builder.Logging.AddLocalFileLogger(options => { options.SaveDays = 7; });

builder.Services.AddHttpClient<BitcoinPriceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//�h�y��
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var currentCulture = CultureInfo.CurrentCulture;
    var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("zh-TW"),
            new CultureInfo("en"),
        };
    options.DefaultRequestCulture = new RequestCulture(currentCulture);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    // �b�^�����Y���[�J Content-Language ���Y�A�i���Τ�ݦ��� HTTP ���e���y������
    options.ApplyCurrentCultureToResponseHeaders = true;
});

//builder.Services.AddDataProtection();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    // API �A��²��
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "���O����� API",
        Description = "���O����� API"
    });
});

var app = builder.Build();
// �`�J Middleware �����{�ǡA���� HTTP Request ��T
app.UseMiddleware<ExceptionMiddleware>();

app.UseRequestLocalization();


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
