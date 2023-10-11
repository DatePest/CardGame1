using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class Shuffle 
{
    private static System.Random rand = new System.Random();

    public static void Shuffle_List<T>(this IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}
