using MidtermAPI_JohnstanleyAjagu.Middlewares;
using MidtermAPI_JohnstanleyAjagu.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<JAProductService, JAFakeProductService>();

var usageCounts = new Dictionary<string, int>();
builder.Services.AddSingleton(usageCounts);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
    app.UseMiddleware<JAApiKeyMiddleware>();

app.UseMiddleware<JAGlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
