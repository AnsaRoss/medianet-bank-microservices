using CuentaMovimiento.Api.Data;
using CuentaMovimiento.Api.Middlewares;
using CuentaMovimiento.Api.Repositories;
using CuentaMovimiento.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CuentaMovimientoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddScoped<IReporteRepository, ReporteRepository>();
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();
builder.Services.AddScoped<IReporteService, ReporteService>();

builder.Services.AddHttpClient<IClienteHttpService, ClienteHttpService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:ClientePersonaUrl"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();


