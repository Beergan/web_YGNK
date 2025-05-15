using HtmlAgilityPack;
using System.Text;
using System.Web; 
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class HtmlDocumentAsync 
{
    public static async Task<IHtmlString> GenerateTOCAsync(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            return new HtmlString("");

        return await Task.Run(() =>
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var nodes = htmlDoc.DocumentNode.SelectNodes("//h2|//h3");
            if (nodes == null)
                return new HtmlString("");

            var tocHtml = new StringBuilder();
            var existingIds = new HashSet<string>();
            var currentLevel = 0;

            tocHtml.Append("<ul class=\"ez-toc-list\">");
            foreach (var node in nodes)
            {
                var titleText = node.InnerText.Trim();
                if (string.IsNullOrWhiteSpace(titleText))
                    continue;

                var id = node.SelectSingleNode(".//span[@id]")?.GetAttributeValue("id", null) ?? node.Id;
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = GenerateSafeId(titleText, existingIds);
                    node.SetAttributeValue("id", id);
                }
                else
                {
                    existingIds.Add(id);
                }

                var level = node.Name == "h2" ? 2 : 3;
                var encodedTitle = HttpUtility.HtmlEncode(titleText);

                if (level > currentLevel)
                {
                    tocHtml.Append("<ul>");
                }
                else if (level < currentLevel)
                {
                    tocHtml.Append("</ul>");
                }

                tocHtml.Append($"<li class=\"ez-toc-heading-level-{level}\"><a class=\"ez-toc-link\" href=\"#{id}\" title=\"{encodedTitle}\">{encodedTitle}</a></li>");

                currentLevel = level;
            }

            while (currentLevel > 0)
            {
                tocHtml.Append("</ul>");
                currentLevel--;
            }

            return new HtmlString(tocHtml.ToString());
        });
    }

    public static string GenerateSafeId(string titleText, HashSet<string> existingIds)
    {
        var safeId = Regex.Replace(titleText.Trim(), @"[^a-zA-Z0-9_-]", "-")
            .Replace("--", "-")
            .ToLower()
            .Trim('-');

        if (safeId.Length > 0 && char.IsDigit(safeId[0]))
            safeId = "_" + safeId;

        var baseId = safeId;
        int suffix = 1;
        while (existingIds.Contains(safeId))
        {
            safeId = $"{baseId}-{suffix++}";
        }
        existingIds.Add(safeId);
        return safeId;
    }
    
}