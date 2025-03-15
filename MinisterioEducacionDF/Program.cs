using AppDbContext;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using Scalar.AspNetCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<MinisterioEducacionDfContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FinanzasConnection"),
    opt => opt.MigrationsAssembly("MinisterioEducacionDF")));

builder.Services
    .AddScoped<ICuentaRepository, CuentaRepository>()
    .AddScoped<ITransaccionRepository, TransaccionRepository>()
    .AddScoped<ICuentaService, CuentaService>()
    .AddScoped<ITransaccionService, TransaccionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Módulo Finanzas";
        options.Theme = ScalarTheme.BluePlanet;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.CustomCss = string.Empty;
        options.ShowSidebar = true;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
