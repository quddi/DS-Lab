using System;
using System.Collections.Generic;

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

    public static bool ContainsCopies<T>(this T[,] arr, HashSet<T> except)
    {
        var foundValues = new HashSet<T>();

        var rows = arr.GetLength(0);
        var columns = arr.GetLength(1);
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var value = arr[i, j];
                
                if (!except.Contains(value) &&foundValues.Contains(value))
                    return true;

                foundValues.Add(value);
            }
        }

        return false;
    }

    public static int[,] GetRandomMatrix(int rows, int columns, int minValue = -99, int maxValue = 99)
    {
        var result = new int[rows, columns];

        var usedValues = new HashSet<int>();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (GetRandomSuccess(50))
                {
                    result[i, j] = 0;
                    continue;
                }

                var randomValue = Random.Next(minValue, maxValue + 1);
                
                while (usedValues.Contains(randomValue))
                    randomValue = Random.Next(minValue, maxValue + 1);

                result[i, j] = randomValue;
                usedValues.Add(randomValue);
            }
        }

        return result;
    } 
}