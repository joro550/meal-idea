
namespace meal_ideas.Meals.Data;

public sealed class Meal
{
    public string Name { get; init; }
        = string.Empty;

    public List<Ingredient> IngredientList { get; init; } = new();
    public List<Instructions> InstructionList { get; init; } = new();
}