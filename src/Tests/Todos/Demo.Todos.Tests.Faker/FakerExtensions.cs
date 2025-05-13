using System.Reflection;
using Bogus;

public static class FakerExtensions
{
    private static readonly string[] Directions = { "asc", "desc" };

    /// <summary>
    /// Generates a random order clause based on the properties of the given entity type.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <param name="faker">The Faker instance.</param>
    /// <returns>A random order clause (e.g., "Name asc", "CreatedAt desc").</returns>
    public static string GenerateOrder<T>(this Faker faker)
    {
        var properties = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType == typeof(string) || p.PropertyType.IsValueType)
            .Select(p => p.Name)
            .ToArray();

        if (properties.Length == 0)
            throw new InvalidOperationException($"No valid properties found in {typeof(T).Name} for sorting.");

        string column = faker.PickRandom(properties);
        string direction = faker.PickRandom(Directions);

        return $"{column} {direction}";
    }
}
