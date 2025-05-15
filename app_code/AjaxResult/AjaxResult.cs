using System;
using System.Web;

public enum StatusCode : int
{
    Ok = 200,
    Created = 201,
    BadRequest = 400,
    Duplicated = 403,
    NotFound = 404,
    Fail = 500
}

public static class AjaxResult
{ 
    private static Action MakeResponse(int status, string data)
    {
        var response = HttpContext.Current.Response;
        response.StatusCode = status;
        response.Write(data);
        return () => response.End();
    }

    public static Action Ok(string description = null) => MakeResponse(200, description);

    public static Action BadRequest(string error = "Bad request!") => MakeResponse(400, error);

    public static Action NotAuthenticated(string error = "You are not authenticated!") => MakeResponse(403, error);

    public static Action Duplicated(string error = "Can not insert because duplicated item found!") => MakeResponse(403, error);

    public static Action NotFound(string error = "Process failed befause item not found!") => MakeResponse(404, error);

    public static Action Fail(string error = "Process failed with internal error!") => MakeResponse(500, error);
}