using ColiTool.Database;
using ColiTool.Database.Repositories;
using ColiTool.WebAPI.Hubs;
using ColiTool.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repository
builder.Services.AddScoped<ICanMessageRepository, CanMessageRepository>();

// Add SignalR
builder.Services.AddSignalR();

// Register CAN Bus service
builder.Services.AddSingleton<CanBusService>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

// Simulate CAN Bus events
var canBusService = app.Services.GetRequiredService<CanBusService>();
canBusService.SimulateCanBusEvents();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<CanBusHub>("/canbusHub");

// Serve the html file for the SignalR client
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
