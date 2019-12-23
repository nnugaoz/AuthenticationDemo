using System.Web;
public class T_CarTeam : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
