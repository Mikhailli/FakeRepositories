using System;

namespace FakeRepositories;

public static class Utils
{
    // Корректирует склонение
    public static string ConvertSecondsToString(int seconds)
    {
        if (seconds <= 0)
        {
            throw new ArgumentException($"Количество секунд должно быть положительным.");
        }

        if (seconds >= 86400)
            return $"{seconds / 86400} {CorrectDays(seconds / 86400)}, " +
                   $"{seconds % 86400 / 3600} {CorrectHours(seconds % 86400 / 3600)}, " +
                   $"{seconds % 3600 / 60} {CorrectMinutes(seconds % 3600 / 60)}, " +
                   $"{seconds % 60} {CorrectSeconds(seconds % 60)}";
        
        if (seconds >= 3600)
            return $"{seconds / 3600} {CorrectHours(seconds / 3600)}, " +
                   $"{seconds % 3600 / 60} {CorrectMinutes(seconds % 3600 / 60)}, " +
                   $"{seconds % 60} {CorrectSeconds(seconds % 60)}";
        
        if (seconds >= 60)
            return $"{seconds / 60} {CorrectMinutes(seconds / 60)}, " +
                   $"{seconds % 60} {CorrectSeconds(seconds % 60)}";
        
        return $"{seconds} {CorrectSeconds(seconds)}";
    }

    private static string CorrectDays(int count)
    {
        if (count % 100 == 11 || count % 100 == 12 || count % 100 == 13 || count % 100 == 14)
            return "дней";

        if (count % 10 == 5 || count % 10 == 6 || count % 10 == 7 || count % 10 == 8 || count % 10 == 9 || count % 10 == 0)
            return "дней";

        if (count == 1)
            return "день";

        return "дня";
    }
    
    private static string CorrectHours(int count)
    {
        if (count % 100 == 11 || count % 100 == 12 || count % 100 == 13 || count % 100 == 14)
            return "часов";

        if (count % 10 == 5 || count % 10 == 6 || count % 10 == 7 || count % 10 == 8 || count % 10 == 9 || count % 10 == 0)
            return "часов";

        if (count == 1)
            return "час";

        return "часа";
    }
    
    private static string CorrectMinutes(int count)
    {
        if (count % 100 == 11 || count % 100 == 12 || count % 100 == 13 || count % 100 == 14)
            return "минут";

        if (count % 10 == 5 || count % 10 == 6 || count % 10 == 7 || count % 10 == 8 || count % 10 == 9 || count % 10 == 0)
            return "минут";

        if (count == 1)
            return "минута";

        return "минуты";
    }
    
    private static string CorrectSeconds(int count)
    {
        if (count % 100 == 11 || count % 100 == 12 || count % 100 == 13 || count % 100 == 14)
            return "секунд";

        if (count % 10 == 5 || count % 10 == 6 || count % 10 == 7 || count % 10 == 8 || count % 10 == 9 || count % 10 == 0)
            return "секунд";

        if (count == 1)
            return "секунда";

        return "секунды";
    }
}