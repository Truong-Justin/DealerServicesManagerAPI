global using DealerServicesManagerAPI.Models;
global using Microsoft.EntityFrameworkCore;
global using DealerServicesManagerAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DealerServicesDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CONNECTION")));

builder.Services.AddSingleton<ICustomerServicesRepository, CustomerServicesRepository>();
builder.Services.AddSingleton<IDealerServicesRepository, DealerServicesRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

