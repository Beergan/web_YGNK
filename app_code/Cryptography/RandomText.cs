using System;

public static class RandomText
{
    public static string Generate()
    {
        // Generate random text
        string s = "";
        char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        int index;
        int lenght = RNG.Next(4, 6);
        for (int i = 0; i < lenght; i++)
        {
            index = RNG.Next(chars.Length - 1);
            s += chars[index].ToString();
        }

        return s;
    }
}