using RabbitMQ.Publisher.Interfaces;
using RabbitMQ.Publisher.Models;
using RabbitMQ.Publisher.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.
builder.Services.AddOptions();
builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitConfigs"));

builder.Services.AddScoped<IMessageBrokerConnectionService, MessageBrokerConnectionService>();
builder.Services.AddScoped<IMessageBrokerService, MessageBrokerService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiDocument();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
