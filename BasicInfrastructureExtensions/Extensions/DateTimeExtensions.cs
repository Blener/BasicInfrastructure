using System;
using System.Globalization;

namespace BasicInfrastructureExtensions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        
        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            return new GregorianCalendar().AddDays(date, -((int)date.DayOfWeek));
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            return new GregorianCalendar().AddDays(date, -((int)date.DayOfWeek)).AddDays(7);
        }
    }
}
