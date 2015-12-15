using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Web.UI.WebControls;

namespace App.Extensions
{
    /// <summary>
    /// Extension Methods to simplify common patterns
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Solution presented by Tejs on stackoverflow.com
        /// http://stackoverflow.com/questions/5733010/filter-entities-then-sort-on-dynamic-field
        /// </summary>
        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source, string propertyName, SortDirection direction = SortDirection.Ascending)
        {
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentException("The propertyName parameter is null or empty. A valid propertyName must be supplied.");
            var sourceType = typeof (T);
            var hasProperty = sourceType.GetProperty(propertyName) != null;
            if (!hasProperty) throw new ArgumentException(string.Format("There is no property named '{0}' for the {1} type.", propertyName, sourceType));
            return direction == SortDirection.Ascending 
                ? source.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null)) 
                : source.OrderByDescending(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }

        /// <summary>
        /// Extends the string type with a method that scrubs a string to 
        /// remove special characters.
        /// </summary>
        /// <param name="s">The string the method is extending</param>
        /// <example>
        /// <code>
        /// <![CDATA[
        ///     string sample = "<tag/>";
        ///     sample = sample.ParseForXml();
        ///     
        /// ]]>    
        /// </code>
        /// <code>
        /// <![CDATA[End Result: sample = "&lt;tag/&gt;"]]>
        /// </code>
        /// </example>
        /// <returns>A string that has all problem characters scrubbed.</returns>
        public static string ParseForXml(this string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace("&", "&amp;");
                s = s.Replace("<", "&lt;");
                s = s.Replace(">", "&gt;");
                s = s.Replace("'", "&#39;");
                s = s.Replace("\"", "&quot;");
            }
            else
            {
                s = "";
            }
            return s;
        }

        private static int ParseAsString(object o)
        {
            var result = -1;
            var oString = o.ToString();
            if (!string.IsNullOrEmpty(oString))
            {
                if (oString == "0")
                {
                    result = 0;
                }
                else
                {
                    int.TryParse(oString, out result);
                    if (result == 0) result = -1;
                }
            }
            return result;
        }

        /// <summary>
        /// Converts an object to an integer. (Default = -1)
        /// </summary>
        /// <param name="o">Object to be parsed as an integer</param>
        /// <returns>
        /// Returns the object as an integer if the object can be parsed. 
        /// If the object cannot be parsed a -1 is returned.
        /// </returns>
        public static int ToInt(this object o)
        {
            if (o == null) return -1;
            try
            {
                var convertedResult = Convert.ToInt32(o);
                if (convertedResult > 0) return convertedResult;
            }
            catch (FormatException)
            {
                return ParseAsString(o);
            }
            catch (InvalidCastException)
            {
                return -1;
            }
            return ParseAsString(o);
        }
        
        /// <summary>
        /// Converts an object to an integer and allows a default value to be passed in.
        /// </summary>
        /// <param name="o">Object to be parsed as an integer</param>
        /// <param name="defaultVal"></param>
        /// <returns>
        /// Returns the object as an integer if the object can be parsed. 
        /// If the object cannot be parsed the default value is returned.
        /// </returns>
        public static int ToInt(this object o, int defaultVal)
        {
            if (o != null)
                if (o.ToString() == "-1") return -1;

            var result = o.ToInt();
            if (result == -1) return defaultVal;
            return result;
        }

        /// <summary>
        /// Converts an object to a 64 bit integer. (Default = -1)
        /// </summary>
        /// <param name="o">Object to be parsed as a 64 bit integer</param>
        /// <returns>
        /// Returns the object as a 64 bit integer if the object can be parsed. 
        /// If the object cannot be parsed a -1 is returned.
        /// </returns>
        public static Int64 ToInt64(this object o)
        {
            if (o == null) return -1;
            try
            {
                var convertedResult = Convert.ToInt64(o);
                if (convertedResult > 0) return convertedResult;
            }
            catch (FormatException)
            {
                return ParseAsString(o);
            }
            catch (InvalidCastException)
            {
                return -1;
            }
            return ParseAsString(o);
        }

        /// <summary>
        /// Converts an object to a nullable integer. (Default = null)
        /// </summary>
        /// <param name="o">Object to be parsed as an integer</param>
        /// <returns>
        /// Returns the object as a nullable integer if the object can be parsed. 
        /// If the object cannot be parsed null is returned.
        /// </returns>
        public static int? ToIntNullable(this object o)
        {
            int? result = null;
            if (o != null)
                if (o.ToString() == "-1") return -1;

            int parseResult = o.ToInt();
            if (parseResult != -1)
            {
                result = parseResult;
            }

            return result;
        }
        
        /// <summary>
        /// Converts an object to a DateTime. (Default = DateTime.MinValue)
        /// </summary>
        /// <param name="o">Object to be parsed as a DateTime</param>
        /// <returns>
        /// Returns the object as a DateTime if the object can be parsed. 
        /// If the object cannot be parsed DateTime.MinValue is returned.
        /// </returns>
        public static DateTime ToDateTime(this object o)
        {
            if (o == null) return DateTime.MinValue;
            
            DateTime result;
            DateTime.TryParse(o.ToString(), out result);
            return result;
        }
        
        /// <summary>
        /// Replaces instances of "\r\n" with "<br />" in a string.
        /// </summary>
        /// <param name="s">The string that needs its line breaks modified.</param>
        /// <returns>A the original string containing no "\r\n" character codes since they were all replaced with "<br />".</returns>
        public static string ConvertLineBreaksToHtml(this string s)
        {
            return s.Replace("\r\n", "<br />"); 
        }

        /// <summary>
        /// Replaces instances of HTML BR tags with "\r\n" line break characters
        /// </summary>
        /// <param name="s">The string that needs br tags converted</param>
        /// <returns>The original string containing no br tags because they have been replaced by "\r\n".</returns>
        public static string ConvertHtmlToLineBreaks(this string s)
        {
            string result = s;
            var brs = new[] { "<br/>", "<BR/>", "<br />", "<BR />", "<br>", "<BR>" };
            foreach (string br in brs)
            {
                result = result.Replace(br, "\r\n");
            }
            return result;
        }

        /// <summary>
        /// Returns the original string or a default value if it is null or empty
        /// </summary>
        /// <param name="s">The string to test</param>
        /// <param name="defaultVal">The default value to set the string to if it is null or empty</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this string s, string defaultVal)
        {
            string result = s;
            if (string.IsNullOrWhiteSpace(result))
            {
                result = defaultVal;
            }
            return result;
        }

        /// <summary>
        /// Returns the original object as a string or a default value if the object string is null or empty
        /// </summary>
        /// <param name="o">The object to test</param>
        /// <param name="defaultVal">The default value to set the string to if it is null or empty</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this object o, string defaultVal)
        {
            if (o == null) return defaultVal;
            if (string.IsNullOrWhiteSpace(o.ToString())) return defaultVal;
            
            return o.ToString();
        }

        /// <summary>
        /// Replaces Y with Yes and N with No
        /// </summary>
        /// <param name="s">The string to modify.</param>
        /// <returns>"Yes", "No", or the original string if it doesn't translate.</returns>
        public static string YNToYesNo(this string s)
        {
            if (s == null) return "";

            string result;
            switch (s.ToUpper())
            {
                case "Y":
                    result = "Yes";
                    break;
                case "N":
                    result = "No";
                    break;
                default:
                    result = s;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Determines if a string can be parsed to a date
        /// </summary>
        /// <param name="dateString">The string being checked</param>
        /// <returns>True or False</returns>
        private static bool IsDate(string dateString)
        {
            bool result = false;
            string dateToTest = dateString;
            DateTime dt;
            if (DateTime.TryParse(dateToTest, out dt))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Determines if a string can be parsed to a date
        /// </summary>
        /// <param name="dateString">The string being checked</param>
        /// <returns>True or False</returns>
        public static bool IsValidDate(this string dateString)
        {
            return IsDate(dateString);
        }

        /// <summary>
        /// Determines if a string meets a specified date format
        /// </summary>
        /// <param name="dateString">The string being checked</param>
        /// <param name="dateFormat">The date format it must conform to</param>
        /// <returns></returns>
        public static bool IsValidDate(this string dateString, string dateFormat)
        {
            if (string.IsNullOrEmpty(dateFormat)) return dateString.IsValidDate();
            
            bool result = false;
            DateTime dtTest;
            var enUS = new CultureInfo("en-US");
            if (DateTime.TryParseExact(dateString, dateFormat, enUS, DateTimeStyles.None, out dtTest))
            {
                result = true;
            }
            if (result)
            {
                result = IsDate(dateString);
            }
            return result;
        }

        /// <summary>
        /// Convert a string to a bool based on an expected true string value
        /// </summary>
        /// <param name="s">Some string</param>
        /// <param name="trueValue">Expected true value</param>
        /// <returns></returns>
        public static bool ToBooleanByTrueVal(this string s, string trueValue)
        {
            return (s == trueValue);
        }

        /// <summary>
        /// Applies an Action procedure to any enumeration
        /// </summary>
        /// <typeparam name="T">Enumerable type</typeparam>
        /// <param name="enumeration">The Enumeration</param>
        /// <param name="action">The procedure to perform on each item in the enumeration</param>
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        /// <summary>
        /// Converts a date to short string or "N/A" if the date equals DateTime.MinValue
        /// </summary>
        /// <param name="theDate">The date to convert</param>
        /// <returns>A short date string representing the date or "N/A"</returns>
        public static string ToShortDateStringOrNA(this DateTime theDate)
        {
            return theDate.ToShortDateStringOrNA("N/A");
        }

        /// <summary>
        /// Converts a date to short string or a specified default value if the date equals DateTime.MinValue
        /// </summary>
        /// <param name="theDate">The date to convert</param>
        /// <param name="defaultVal">The value to return if the date equals DateTime.MinValue</param>
        /// <returns>A short date string representing the date or the default value.</returns>
        public static string ToShortDateStringOrNA(this DateTime theDate, string defaultVal)
        {
            string result = defaultVal;
            if (theDate != DateTime.MinValue)
            {
                result = theDate.ToShortDateString();
            }
            return result;
        }

        /// <summary>
        /// Converts a string to title case (capitalizes each character that follows a space)
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <returns>The converted string in title case</returns>
        public static string ToTitleCase(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";

            char[] chars = s.ToLower().ToCharArray();
            var sb = new StringBuilder();
            bool capitalize = true;
            for (int i = 0; i < chars.Length; i++)
            {
                string c = chars[i].ToString();

                if (capitalize)
                {
                    sb.Append(c.ToUpper());
                    capitalize = false;
                }
                else
                {
                    sb.Append(c);
                }

                if (c == " ") capitalize = true;
            }
            return sb.ToString();
        }

        public static string StripDomain(this string s)
        {
            if (s == null) return "";
            return (!s.Contains('\\')) ? s : s.Substring(s.LastIndexOf('\\') + 1);
        }

    }
}
