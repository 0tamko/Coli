using ColiTool.CanBus;
using ColiTool.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CanBusService>();

builder.Services.AddHostedService<CanBackgroundService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();