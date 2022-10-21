using LiteDB.Async;
using meal_ideas.Meals;
using meal_ideas.Migrations;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(x => x.AsScoped(), typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ILiteDatabaseAsync>(_ => new LiteDatabaseAsync("db.db"));
builder.Services.AddSingleton<MigrationExecutor>();
builder.Services.AddSingleton<Random>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.AddMealEndpoints();

var executor = app.Services.GetService<MigrationExecutor>();
if (executor == null)
    throw new ApplicationException("Could not find type");

await executor.ExecuteAsync();

var port = Environment.GetEnvironmentVariable("PORT") ?? null;
app.Run(port == null ? null : $"http://0.0.0.0:{port}");