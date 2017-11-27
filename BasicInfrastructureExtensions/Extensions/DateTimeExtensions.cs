using System;
using System.Globalization;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1);
        }


        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-(int)date.DayOfWeek);
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-(int)date.DayOfWeek).AddDays(6);
        }

        public static DateTime FirstDayOfNextWeek(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-(int)date.DayOfWeek).AddDays(7);
        }

        public static bool IsBefore(this DateTime date, DateTime otherDate)
        {
            return date.CompareTo(otherDate) < 0;
        }

        public static bool IsAfter(this DateTime date, DateTime otherDate)
        {
            return date.CompareTo(otherDate) > 0;
        }
    }
}
