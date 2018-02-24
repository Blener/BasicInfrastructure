using System;
using System.Collections.Generic;
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

        public static DateTime ThisDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
        public static DateTime NextDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(1);
        }
        public static DateTime DayBefore(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day).AddDays(-1);
        }

        public static List<DateTime> NextDays(this DateTime actual, int daysAhead, List<DateTime> list = null)
        {
            if (list == null)
                list = new List<DateTime>();

            list.Add(actual);

            if (daysAhead == 0)
                return list;

            if (daysAhead > 0)
                return NextDays(actual.AddDays(1), --daysAhead, list);

            return NextDays(actual.AddDays(-1), ++daysAhead, list);
        }
        public static List<DateTime> NextWeekDays(this DateTime actual, int daysAhead, List<DateTime> list = null, bool? backwards = null)
        {
            if (list == null)
                list = new List<DateTime>();
            if (backwards == null)
                backwards = daysAhead < 0;

            if (actual.DayOfWeek == DayOfWeek.Saturday)
                actual = actual.AddDays(backwards.Value ? -1 : 2);
            else if (actual.DayOfWeek == DayOfWeek.Sunday)
                actual = actual.AddDays(backwards.Value ? -2 : 1);

            list.Add(actual);

            if (daysAhead == 0)
                return list;

            if (daysAhead > 0)
                return NextWeekDays(actual.AddDays(1), --daysAhead, list, backwards);

            return NextWeekDays(actual.AddDays(-1), ++daysAhead, list, backwards);
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
