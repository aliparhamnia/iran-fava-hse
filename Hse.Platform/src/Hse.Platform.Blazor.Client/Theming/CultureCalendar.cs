using System;
using System.Globalization;

namespace Hse.Platform.Blazor.Client.Theming;

public static class CultureCalendar
{
    private static readonly PersianCalendar Persian = new();

    public static bool IsPersian(CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentUICulture;
        return culture.TwoLetterISOLanguageName.Equals("fa", StringComparison.OrdinalIgnoreCase);
    }

    public static (int Year, int Month, int Day) ToPersian(DateOnly date)
    {
        var dateTime = date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Unspecified);
        return (Persian.GetYear(dateTime), Persian.GetMonth(dateTime), Persian.GetDayOfMonth(dateTime));
    }

    public static DateOnly FromPersian(int year, int month, int day)
    {
        return DateOnly.FromDateTime(Persian.ToDateTime(year, month, day, 0, 0, 0, 0));
    }

    public static int GetDaysInMonth(int year, int month)
    {
        return Persian.GetDaysInMonth(year, month);
    }
}
