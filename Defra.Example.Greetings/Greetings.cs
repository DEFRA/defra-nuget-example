namespace Defra.Example.Greetings;

/// <summary>
/// Provides simple greeting helpers.
/// </summary>
public static class Greeting
{
    /// <summary>
    /// Creates a friendly greeting for the supplied name.
    /// </summary>
    public static string Hello(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name must not be empty.", nameof(name));
        }

        return $"Hello, {name.Trim()}!";
    }
}
