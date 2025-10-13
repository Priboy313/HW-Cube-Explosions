using System;

static class DevUtils
{
    private static Random s_random = new Random();
    private static int s_chanceMin = 0;
    private static int s_chanceMax = 100;

    public static int GetRandomNumber(int min, int max)
    {
        return s_random.Next(min, max);
    }

    public static bool IsChanceSuccess(int chancePercent)
    {
        int chance = Math.Max(s_chanceMin, Math.Min(s_chanceMax, chancePercent));

        return GetRandomNumber(s_chanceMin, s_chanceMax) < chance;
    }
}
