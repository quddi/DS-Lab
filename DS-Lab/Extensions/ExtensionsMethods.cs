using System;

public static class ExtensionsMethods
{
    public static Random Random { get; } = new();

    public static bool GetRandomSuccess(int chance)
    {
        if (chance < 0 || chance > 100)
            throw new ArgumentException(nameof(chance));

        var randomValue = Random.Next(0, 101);

        return chance >= randomValue;
    }

    public static string ToBeatifiedString<TValue>(this TValue value)
    {
        if (value is int i)
        {
            return i / 10 == 0 ? string.Format($" {i}") : i.ToString();
        }

        return string.Empty;
    }
}