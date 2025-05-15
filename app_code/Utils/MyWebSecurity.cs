using System;
using System.Web;

public static class MyWebSecurity
{
	private static readonly md5hash md5h = new md5hash();

	private static bool CheckCookieLogin(string cookie, string ip, string domain)
	{
		try
		{
			string check = md5h.Decrypt(cookie);

			if (check != (("yes" + ip + domain)))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	public static bool CheckAuthenticatedUser()
	{
		var context = HttpContext.Current;

		string cookie = context.Request.Cookies["check"]?["login"]?.ToString();

		if (cookie == null)
			return false;

		string ip = context.Request.ServerVariables["HTTP_CF_CONNECTING_IP"];
		string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);

		return CheckCookieLogin(cookie, ip, domain);
	}

	public static void Logout()
	{
		var context = HttpContext.Current;

		HttpCookie cookie = new HttpCookie("check");
		cookie["lang"] = "vn";
		cookie["user"] = "";
		cookie["displayName"] = string.Empty;
		cookie["login"] = "";
		cookie.Expires = DateTime.Now.AddDays(1d);
		context.Response.Cookies.Add(cookie);
		context.Response.Redirect(Constants.Admin_Login_Url);
	}

	public static void WriteAuthenCookie(string userId, string password, string displayName)
	{
		var context = HttpContext.Current;
		string ip = context.Request.ServerVariables["HTTP_CF_CONNECTING_IP"];
		string domain = context.Request.Url.GetLeftPart(UriPartial.Authority);

		HttpCookie login = new HttpCookie("check");
		login["lang"] = "vn";
		login["user"] = userId;
		login["displayName"] = EncodeHelper.EncodeBase64(displayName);
		login["login"] = md5h.Encrypt("yes" + ip + domain);
		login.Expires = DateTime.Now.AddDays(1);
		context.Response.Cookies.Add(login);
	}
}