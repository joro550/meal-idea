using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace meal_ideas;

public interface IRepository<T> where T : new()
{
    public Task<List<T>> Get();
}

public sealed class FileRepository<T> : IRepository<T> where T : new()
{
    public Task<List<T>> Get()
    {
        var typeName = typeof(T).Name;
        var directory
            = Path.Combine(Directory.GetCurrentDirectory(), "Migrations", typeName);

        return Task.FromResult(Directory.GetFiles(directory)
            .Select(File.ReadAllText)
            .Select(t => JsonSerializer.Deserialize<List<T>>(t))
            .Where(t => t is { Count: > 0 })
            .Aggregate((cur, next) =>
            {
                cur!.AddRange(next!);
                return cur;
            })!
            .ToList());
    }
}

public sealed class CacheRepository<T> : IRepository<T> where T : new()
{
    private readonly IMemoryCache _memoryCache;
    private readonly FileRepository<T> _fileRepository;

    public CacheRepository(FileRepository<T> fileRepository, IMemoryCache memoryCache)
    {
        _fileRepository = fileRepository;
        _memoryCache = memoryCache;
    }

    public async Task<List<T>> Get() =>
        await _memoryCache.GetOrCreateAsync(typeof(T).Name, 
            async _ => await _fileRepository.Get());
}