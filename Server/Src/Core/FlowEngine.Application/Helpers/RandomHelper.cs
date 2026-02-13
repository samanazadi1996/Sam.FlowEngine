using System;
using System.IO;
using System.Linq;

namespace FlowEngine.Application.Helpers;

public static class RandomHelper
{
    private static readonly Random random = new();

    public static string RandomString(int length = 8)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }

    public static int RandomInt(int min = 0, int max = 1000)
    {
        return random.Next(min, max);
    }

    public static bool RandomBool()
    {
        return random.Next(0, 2) == 1;
    }

    public static string GetProfileImage()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-images");

        if (!Directory.Exists(path))
            return null;

        var files = Directory.GetFiles(path);

        if (files.Length == 0)
            return null;

        var rnd = new Random();
        var randomFile = files[rnd.Next(files.Length)];

        return Path.GetFileName(randomFile);
    }

}
