using System.Text.Json;
using LiteDB.Async;
using meal_ideas.Meals.Data;

namespace meal_ideas.Migrations;

public class MigrationExecutor
{
    private readonly ILiteDatabaseAsync _liteDatabase;

    public MigrationExecutor( ILiteDatabaseAsync liteDatabase) 
        => _liteDatabase = liteDatabase;

    public async Task ExecuteAsync()
    {
        var mealCollection = _liteDatabase.GetCollection<Meal>();
        var databaseMeals = await mealCollection.Query().ToListAsync();
        
        var mealDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", "meals");

        foreach (var mealFile in Directory.GetFiles(mealDirectory))
        {
            var fileContent = await File.ReadAllTextAsync(mealFile);
            var mealFileCollection = JsonSerializer.Deserialize<List<Meal>>(fileContent);
            if (mealFileCollection == null || !mealFileCollection.Any()) 
                continue;
            
            foreach (var meal in mealFileCollection.Where(meal => databaseMeals.All(x => x.Name != meal.Name)))
            {
                await mealCollection.InsertAsync(meal);
            }
        }
    }
}