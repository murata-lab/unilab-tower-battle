using System;

public static class Extensions
{
    public static T[] Appended<T>(this T[] array, T item)
    {
        if (array == null)
        {
            return new T[] { item };
        }
        Array.Resize(ref array, array.Length + 1);
        array[array.Length - 1] = item;

        return array;
    }
}