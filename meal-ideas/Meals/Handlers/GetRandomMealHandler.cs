using MediatR;
using LiteDB.Async;
using meal_ideas.Meals.Data;
using meal_ideas.Meals.Request;

namespace meal_ideas.Meals.Handlers;

public class GetRandomMealHandler : IRequestHandler<GetRandomMeal, IResult>
{
    private readonly Random _random;
    private readonly ILiteDatabaseAsync _databaseAsync;

    public GetRandomMealHandler(ILiteDatabaseAsync databaseAsync, Random random)
    {
        _databaseAsync = databaseAsync;
        _random = random;
    }

    public async Task<IResult> Handle(GetRandomMeal request, CancellationToken cancellationToken)
    {
        var collection = _databaseAsync.GetCollection<Meal>();
        var meals = await collection.Query().ToListAsync();

        var randomNumber = _random.Next(0, meals.Count);
        return Results.Ok(meals[randomNumber]);
    }
}