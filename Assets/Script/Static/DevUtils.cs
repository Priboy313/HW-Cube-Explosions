using UnityEngine;

static class DevUtils
{
    private static float s_chanceMax = 100f;

    public static int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    public static bool IsChanceSuccess(int chancePercent)
    {
        return Random.value < (chancePercent / s_chanceMax);
    }
}
