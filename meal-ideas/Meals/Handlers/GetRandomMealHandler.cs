using MediatR;
using meal_ideas.Meals.Data;
using meal_ideas.Meals.Request;

namespace meal_ideas.Meals.Handlers;

public sealed class GetRandomMealHandler : IRequestHandler<GetRandomMeal, IResult>
{
    private readonly Random _random;
    private readonly IRepository<Meal> _mealRepository;

    public GetRandomMealHandler(IRepository<Meal> mealRepository, Random random)
    {
        _random = random;
        _mealRepository = mealRepository;
    }

    public async Task<IResult> Handle(GetRandomMeal request, CancellationToken cancellationToken)
    {
        var meals = await _mealRepository.Get();
        var randomNumber = _random.Next(0, meals.Count);
        return Results.Ok(meals[randomNumber]);
    }
}