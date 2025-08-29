using System.Text.RegularExpressions;

namespace JobFinder.DataAccess.Utilities;

public static class EntityNameBeautifier
{
    public static string Beautify(this string entityName)
    {
        string truncated = entityName.TrimEnd("Entity".ToCharArray());

        Regex rgx = new Regex(@"[A-Z]+[a-z]+");

        MatchCollection matches = rgx.Matches(truncated);

        string beautified = "";
        foreach (Match match in matches)
        {
            beautified += match.Value + " ";
        }

        return beautified.TrimEnd(' ');
    }
}
