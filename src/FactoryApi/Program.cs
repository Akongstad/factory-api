using FactoryApi.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PGDefaultConnection");
builder.Services.AddDbContext<StateDbContext>(options =>
    options.UseNpgsql(connectionString));
// builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// app.MapStateEndpoints();

app.MapGet("/", () => "Hello World!");

app.Run();