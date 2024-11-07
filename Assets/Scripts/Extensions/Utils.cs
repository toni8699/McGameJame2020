using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Utils
{
    public static bool IsEqual(float f1, float f2)
    {
        return Math.Abs(f1 - f2) < 1E-4;
    }

    public static bool IsEqual(double d1, double d2)
    {
        return Math.Abs(d1 - d2) < 1E-4;
    }

    /*
     * f1 <= f2
     */
    public static bool LessOrEqual(float f1, float f2)
    {
        return IsEqual(f1, f2) || f1 < f2;
    }

    public static bool LessOrEqual(double d1, double d2)
    {
        return IsEqual(d1, d2) || d1 < d2;
    }

    public static void Shuffle(this GameObject[] array)
    {
        var count = array.Length;

        for (var i = 0; i < count; i++)
        {
            var index1 = Random.Range(0, count - 1);
            var index2 = Random.Range(0, count - 1);

            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }

    public static GameObject GetRandomElement(this GameObject[] array)
    {
        var index = Random.Range(0, array.Length - 1);

        return array[index];
    }

}
