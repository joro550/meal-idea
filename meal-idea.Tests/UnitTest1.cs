using System.Text.Json;
using System.Xml.Serialization;
using meal_ideas.Meals.Data;

namespace meal_idea.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var meals = new Meal()
        {
            Name = "Chicken Korma",
            IngredientList = new List<Ingredient>
            {
                new() { Name = "Vegetable oil" },
                new() { Name = "Chicken thigh filltes" },
                new() { Name = "Indian base mix" },
                new() { Name = "Korma paste" },
                new() { Name = "Chikpeas" },
                new() { Name = "Chicken stock" },
                new() { Name = "Butternut Squash" },
                new() { Name = "Almonds" },
                new() { Name = "Sultanas" },
                new() { Name = "Raisins" },
                new() { Name = "Corriander" },
            }
        };

        var thing = JsonSerializer.Serialize(meals);

    }
}