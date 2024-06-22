using Microsoft.EntityFrameworkCore;
using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.DomainServices.Interfaces;
using CustomerSupportManagement.DomainServices.Services;
using CustomerSupportManagement.Infrastructure.Middleware;
using CustomerSupportManagement.Infrastructure.SQLRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISupportAgentService, SupportAgentService>();

builder.Services.AddScoped<ISupportAgentRepo, SQLSupportAgentRepo>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var configuration = builder.Configuration;
var connectionString = configuration["WeBall:MySQLDBConn"];
builder.Services.AddDbContext<SQLDbContext>(opts =>
{
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// # SupportAgent #
app.MapGet("/supportagents", async (ISupportAgentService supportAgentService) => await supportAgentService.GetAll());

app.MapGet("/supportagents/{id}", async (ISupportAgentService supportAgentService, string id) => await supportAgentService.GetById(id));

app.MapPost("/supportagents", async (ISupportAgentService supportAgentService, SupportAgent supportAgent) =>
{
    await supportAgentService.Create(supportAgent);
    return Results.Created($"/supportagents/{supportAgent.Id}", new { code = "201", message = "SupportAgent created successfully" });
});

app.MapPut("/supportagents/{id}", async (ISupportAgentService supportAgentService, string id, SupportAgent supportAgent) =>
{
    await supportAgentService.Update(id, supportAgent);
    return Results.Ok(new { code = "200", message = "SupportAgent updated successfully" });
});

app.MapDelete("/supportagents/{id}", async (ISupportAgentService supportAgentService, string id) =>
{
    await supportAgentService.Delete(id);
    return Results.Ok(new { code = "200", message = "SupportAgent deleted successfully" });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();