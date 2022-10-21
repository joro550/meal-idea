using MediatR;
using meal_ideas;
using meal_ideas.Meals;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Random>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(x => x.AsScoped(), typeof(Program));

builder.Services.AddTransient(typeof(FileRepository<>));
builder.Services.AddTransient(typeof(IRepository<>), typeof(CacheRepository<>));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.AddMealEndpoints();

var port = Environment.GetEnvironmentVariable("PORT") ?? null;
app.Run(port == null ? null : $"http://0.0.0.0:{port}");