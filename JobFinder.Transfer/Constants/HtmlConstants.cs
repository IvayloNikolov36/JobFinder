namespace JobFinder.Transfer.Constants;

public class HtmlConstants
{
    public static string GetLink(string action, string url) =>
        $@"
            <html>{action} for Job Finder, please click <a href=""{url}"">here</a> or copy and paste the URL in your browser.
                <br>
                <br>
                {url}
                <br>
                <br>
                If you haven't requested this change, please ignore the message.
            </html>
        ";
}
