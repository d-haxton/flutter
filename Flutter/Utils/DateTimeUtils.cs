using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flutter.Utils
{
    public static class DateTimeUtils
    {
        public static string GetPrettyDate(this DateTime postDate)
        {
            var stringy = string.Empty;
            var diff = DateTime.Now.Subtract(postDate);
            double days = diff.Days;
            var hours = diff.Hours + days * 24;
            var minutes = diff.Minutes + hours * 60;
            if (minutes <= 1)
            {
                return "Just Now";
            }

            var years = Math.Floor(diff.TotalDays / 365);
            if (years >= 1)
            {
                return $"{years} year{(years >= 2 ? "s" : null)} ago";
            }

            var weeks = Math.Floor(diff.TotalDays / 7);
            if (weeks >= 1)
            {
                var partOfWeek = days - weeks * 7;
                if (partOfWeek > 0)
                {
                    stringy = $", {partOfWeek} day{(partOfWeek > 1 ? "s" : null)}";
                }

                return $"{weeks} week{(weeks >= 2 ? "s" : null)}{stringy} ago";
            }

            if (days >= 1)
            {
                var partOfDay = hours - days * 24;
                if (partOfDay > 0)
                {
                    stringy = $", {partOfDay} hour{(partOfDay > 1 ? "s" : null)}";
                }

                return $"{days} day{(days >= 2 ? "s" : null)}{stringy} ago";
            }

            if (hours >= 1)
            {
                var partOfHour = minutes - hours * 60;
                if (partOfHour > 0)
                {
                    stringy = $", {partOfHour} minute{(partOfHour > 1 ? "s" : null)}";
                }

                return $"{hours} hour{(hours >= 2 ? "s" : null)}{stringy} ago";
            }

            // Only condition left is minutes > 1
            return minutes.ToString("{0} minutes ago");
        }
    }
}
