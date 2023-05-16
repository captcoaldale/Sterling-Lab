using System;
using System.Linq;
using System.Windows.Forms;
namespace DataObjects
{
    public class Utilties
    {
        int val { get; set; }
        int min { get; set; }
        int max { get; set; }

        // clamp inter values between limits
        public int SetRange(int val, int min, int max)
        {
            val = (val > max) ? min : val;
            val = (val < min) ? max : val;
            return val;
        }
    }

    // extend multiple substrings in contains
    public static class ExtensionMethod
    {
        public static bool ContainsAny(this string str, params string[] values)
        {
            return values.Any(x => str.Contains(x));
        }
    }

    // Adds the given number of business days to the date
    public static class GetBusinessDays
    {
        public static DateTime AddBusinessDays(this DateTime current, int days)
        {
            var sign = Math.Sign(days);
            var unsignedDays = Math.Abs(days);
            for (var i = 0; i < unsignedDays; i++)
            {
                do
                {
                    current = current.AddDays(sign);
                }
                while (current.DayOfWeek == DayOfWeek.Saturday ||
                    current.DayOfWeek == DayOfWeek.Sunday);
            }
            return current;
        }
    

        // Subtracts the given number of business days to the <see cref="DateTime"/>.
        public static DateTime SubtractBusinessDays(this DateTime current, int days)
        {
            return AddBusinessDays(current, -days);
        }
    }

    // autoformat date
    public static class DateTimeMyFormatExtensions
    {
        public static string ToMyFormatString(this DateTime dt, int style=0)
        {
            string output_format = "";
            switch(style)
            {
                case 0:
                    output_format = "MM/dd/yyyy";
                    break;
                case 1:
                    output_format = "yyyy-MMM-dd";
                    break;
                case 2:
                    output_format = "yyyy-MMM-dd hh:mm:ss";
                    break;
                case 3:
                    output_format = "yy-MM-d";
                    break;
            }
            return dt.ToString(output_format);
        }
    }

    public static class StringMyDateTimeFormatExtension
    {
        public static DateTime ParseMyFormatDateTime(this string s,int style=0)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            string output_format = "";
            switch (style)
            {
                case 0:
                    output_format = "yyyy-MMM-dd";
                    break;
                case 1:
                    output_format = "yyyy-MMM-dd hh:mm:ss";
                    break;
                case 2:
                    output_format = "MM/dd/yyyy";
                    break;
                case 3:
                    output_format = "yy-MM-d";
                    break;
            }
            return DateTime.ParseExact(s, output_format, culture);
        }
    }

    // limit a number to X digits after the decimal
    public class SetDecimal
    {
        double Value;
        int d_point;

        public double SetDecimalPoint(string value, int decimalpoint)
        {
            try
            {
                double d = 0;
                // ensure a valid conversion
                if (double.TryParse(value, out d))
                {
                    Value = d;
                    d_point = decimalpoint;
                    Value = Value * Math.Pow(10, d_point);
                    Value = Math.Floor(Value);
                    Value = Value / Math.Pow(10, d_point);
                }
                else
                    Value = -0.00;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "SetDecimalPoint Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Value;
        }

    }

    // clamp string length...
    public static class LimitTo
    {
        public static string Limit(this string data, int length)
        {
            return (data == null || data.Length < length) ? data : data.Substring(0, length);
        }
    }

    public class GetPiece
    {
        public string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            return "";
        }
    }
}
