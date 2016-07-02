using System;

namespace AudioSharp.Utils
{
    public static class FormattingUtils
    {
        public static string FormatString(string stringToBeFormatted, int customNumber)
        {
            DateTimeOffset today = DateTime.Now;
            string result = stringToBeFormatted.Replace("{n}", customNumber.ToString("D4"))
            .Replace("{yyyy}", today.ToString("yyyy"))
            .Replace("{yy}", today.ToString("yy"))
            .Replace("{MMMM}", today.ToString("MMMM"))
            .Replace("{MMM}", today.ToString("MMM"))
            .Replace("{M}", today.ToString("MM"))
            .Replace("{dddd}", today.ToString("dddd"))
            .Replace("{ddd}", today.ToString("ddd"))
            .Replace("{d}", today.ToString("dd"))
            .Replace("{H}", today.ToString("HH"))
            .Replace("{h}", today.ToString("hh"))
            .Replace("{m}", today.ToString("mm"))
            .Replace("{s}", today.ToString("ss"))
            .Replace("{t}", today.ToString("tt"));
            return result;
        }
    }
}
