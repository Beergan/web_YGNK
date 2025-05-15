using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Helpers;

public class MyAdminPage : System.Web.WebPages.WebPage
{
    public override void Execute() { }

    protected override void InitializePage()
    {
        base.InitializePage();
        this.Server.ClearError();
        this.Response.TrySkipIisCustomErrors = true;
    }

    protected Action CheckAuthen()
    {
        var authen = MyWebSecurity.CheckAuthenticatedUser();

        if (authen)
            return EmptyAction;

        if (IsAjax)
        {
            this.Response.StatusCode = 401;
            this.Response.Write("Bạn chưa đăng nhập hoặc phiên đã hết hạn!");
            return () => Response.End();
        }
        else
        {
            return () => Response.Redirect(Constants.Admin_Login_Url, endResponse: true);
        }
    }

    protected IDataService Db => Root.Db;

    public static AdminTranslator Text => new AdminTranslator(LangIdDisplay);
	protected Dictionary<string, string> CattextSelector(string langId, string nodeType = null)
	{
		var query = Db.Query<PP_Category>().OrderByDescending(x => x.Id);



		var items = query
			.OrderBy(o => o.Id)
			.ToList();

		var options = new Dictionary<string, string>() { { "0", Text["*Uncategorized*", "*Không có chuyên mục*"] }, };
		//items.ForEach(i => options.Add(i.Id.ToString(), i.Title));

		var matrix = new List<int>[items.Count];


		for (int i = 0; i < items.Count; i++)
		{

			options.Add(items[i].Id.ToString(), $"{items[i].Title}");


		}

		return options;
	}
	protected Dictionary<string, string> GetGroupSelector(string langId, string nodeType = null)
    {
        var query = Db.Query<PP_Category>(t => t.LangId == langId);

        if (!string.IsNullOrEmpty(nodeType))
        {
            query= query.Where(t => t.NodeType == nodeType);
        }

        var items = query
            .OrderBy(o => o.CategoryPath)
            .ToList();

        var options = new Dictionary<string, string>() { { "0", Text["*Uncategorized*", "*Không có chuyên mục*"] }, };
        //items.ForEach(i => options.Add(i.Id.ToString(), i.Title));

        var matrix = new List<int>[items.Count];

        for (int i = 0; i < items.Count; i++)
        {
            matrix[i] = Enumerable
            .Range(0, items[i].CategoryLevel)
            .Select(t => 0)
            .ToList();

            int col = items[i].CategoryLevel - 1;
            matrix[i][col] = 1;

            for (var j = i - 1; j >= 0; j--)
            {
                if (matrix[j].Count - 1 < col)
                {
                    break;
                }

                if (matrix[j][col] == 1)
                {
                    matrix[j][col] = 2;
                    break;
                }

                matrix[j][col] = 3;
            }
        }

        for (int i = 0; i < items.Count; i++)
        {
            string str = string.Empty;

            for (int j = 0; j < items[i].CategoryLevel; j++)
            {
                switch (matrix[i][j])
                {
                    case 0:
                        str += "\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0";
                        break;
                    case 1:
                        str += "+───\xA0";
                        break;
                    case 2:
                        str += "+───\xA0";
                        break;
                    case 3:
                        str += "│\xA0\xA0\xA0\xA0\xA0\xA0\xA0\xA0";
                        break;
                }
            }

            options.Add(items[i].Id.ToString(), str + $"{items[i].Title} (/{items[i].CategoryPath})");
        }

        return options;
    }

    protected static string AntiForgeryTokenForAjaxPost()
    {
        var antiForgeryInputTag = AntiForgery.GetHtml().ToHtmlString();
        var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
        var tokenValue = removedStart.Replace(@""" />", "");
        if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
            throw new InvalidOperationException("Oops! The Html.AntiForgeryToken() method seems to return something I did not expect.");
        return tokenValue;
    }

    public static string LangIdCompose
    {
        get
        {
            return Root.GetCookieData(nameof(LangIdCompose), setup: () =>
            {
                var langId = "vi";
                var cookie = new HttpCookie(nameof(LangIdCompose));
                cookie.Value = langId;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return langId;
            });
        }
        set
        {
            var cookie = new HttpCookie(nameof(LangIdCompose));
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public static string LangIdDisplay
    {
        get
        {
            return Root.GetCookieData(nameof(LangIdDisplay), setup: () =>
            {
                var langId = "vi";
                var cookie = new HttpCookie(nameof(LangIdDisplay));
                cookie.Value = langId;
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return langId;
            });
        }
        set
        {
            var cookie = new HttpCookie(nameof(LangIdDisplay));
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddYears(1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }

    public static PP_Lang LangCompose => Root.Langs[LangIdCompose];

    protected Action EmptyAction = () => { };

    public string UserId => HttpContext.Current.Request.Cookies["check"]["user"];

    public string DisplayName
    {
        get
        {
            var displayName = HttpContext.Current.Request.Cookies["check"]?["displayName"]?.ToString();

            if (EncodeHelper.IsBase64String(displayName))
                return EncodeHelper.DecodeBase64(displayName);

            return string.Empty;
        }
    }
}