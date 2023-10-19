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

        if (value is string str)
        {
            return str.Length == 1 ? str + " " : str;
        } 

        return string.Empty;
    }

    public static int Clamp(int value, int min, int max)
    {
        if (value < min) return min;

        if (value > max) return max;

        return value;
    }
    
    public static void Print<T>(this T[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write($"{arr[i, j].ToBeatifiedString()}  ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}