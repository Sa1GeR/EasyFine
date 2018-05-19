using System;
using System.Configuration;

namespace PrivateForum.Core.Utilities
{
    public static class DateExtensions
    {
        private static int financialYearFirstMonth = Config.Get<int>("FinancialYearFirstMonth");

        public static int GetYearMonth(this DateTime date)
        {
            return (date.Year * 100) + date.Month;
        }

        public static int MonthsBetween(this DateTime firstDate, DateTime lastDate)
        {
            return (((lastDate.Year - firstDate.Year) * 12) + (lastDate.Month - firstDate.Month));
        }

        public static int MonthsBetween(this DateTime? firstDate, DateTime? lastDate)
        {
            if (!firstDate.HasValue || !lastDate.HasValue) return 0;
            return MonthsBetween(firstDate.Value, lastDate.Value);
        }

        public static int MonthsBetween(this DateTime firstDate, DateTime? lastDate)
        {
            if (!lastDate.HasValue) return 0;
            return MonthsBetween(firstDate, lastDate.Value);
        }

        public static int GetFiscalYear(this DateTime date)
        {
            return (date.Month < financialYearFirstMonth) ?
                date.Year : date.Year + 1;
        }

        public static int GetFiscalMonthPeriod(this DateTime date)
        {
            return (date.Month < financialYearFirstMonth) ?
                date.Month + financialYearFirstMonth - 1: date.Month - financialYearFirstMonth + 1;
        }

        public static DateTime GetNext28DayDate(this DateTime date)
        {
            var date28 = date.AddDays(28 - date.Day);
            return (date.Day >= 28) ? date28.AddMonths(1) /*Get Next Month 28 Date*/ : date28;
        }

        public static DateTime GetFixDayDate(this DateTime date, int day)
        {
            return date.Date.AddDays(day - date.Day);
        }
    }
}