using meal_ideas.Common;

namespace meal_ideas.Meals.Data;

public sealed class Meal : PersistentObject
{
    public string Name { get; init; }
        = string.Empty;

    public List<Ingredient> IngredientList { get; init; } = new();
    public List<Instructions> InstructionList { get; init; } = new();
}