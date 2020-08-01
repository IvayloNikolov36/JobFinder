namespace JobFinder.Web.Infrastructure.Extensions
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string SeparateWords(this string str)
        {
            string pattern = @"[A-Z][a-z]+";

            Regex rgx = new Regex(pattern);

            MatchCollection matches = rgx.Matches(str);

            string output = matches[0].Value;
            for (int i = 1; i < matches.Count; i++)
            {
                output += " " + matches[i].Value; 
            }

            return output;
        }

        public static string ReplaceDashesAndSeparate(this string str)
        {
            string output = str[0].ToString();

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '_')
                {
                    continue;
                }
                else if (char.IsUpper(str[i]) && !char.IsUpper(str[i-1]))
                {
                    output += " " + str[i];
                }
                else
                {
                    output += str[i];
                }
            }

            return output;
        }
    }
}
