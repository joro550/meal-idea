using meal_ideas.Meals.Request;

namespace meal_ideas.Meals;

public static class Endpoints
{
    public static WebApplication AddMealEndpoints(this WebApplication webApplication)
    {
        webApplication.MediateGet<GetRandomMeal>("/meal/random");
        return webApplication;
    }
}