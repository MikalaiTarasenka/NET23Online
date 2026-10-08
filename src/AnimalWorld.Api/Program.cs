using AnimalWorld.Api.DbStuff;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader();
        p.AllowAnyMethod();
        p.WithOrigins("https://localhost:7154");
        p.AllowCredentials();
    });
});

builder.Services.AddDbContext<ApiContext>(op => op.UseNpgsql(builder.Configuration.GetConnectionString("DefaultDbConnection")).UseSnakeCaseNamingConvention());

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");

app.MapGet("GetFacts", (ApiContext dbContext) => dbContext.InterestingFacts.ToList());

app.MapPost("AddFact", (ApiContext dbContext, [FromBody] InterestingFact fact) =>
{
    dbContext.InterestingFacts.Add(fact);
    dbContext.SaveChanges();
    return fact;
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
