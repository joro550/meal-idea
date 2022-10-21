namespace meal_ideas.Meals.Data;

public sealed class Instructions
{
    public int Step { get; init; } = -1;
    
    public string Direction { get; init; } 
        = string.Empty;
}