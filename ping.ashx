<%@ WebHandler Language="C#" Class="ping" %>
using System;
using System.Web;

public class ping : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        VisitCounter.OnRequestPing_New();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}