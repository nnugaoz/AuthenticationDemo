using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class T_CarTeam : IHttpHandler
{
public void ProcessRequest(HttpContext context)
{
string lRequestMethod = context.Request.Params["RequestMethod"].ToString();
switch (lRequestMethod)
{
    case "Insert":
        Insert(context);
        break;
   case "Update":
        Update(context);
         break;
    case "Delete":
        Delete(context);
         break;
     case "Select":
         Select(context);
         break;
     case "SelectPage":
         SelectPage(context);
         break;
}
}
private void Select(HttpContext context)
{
DataTable lDT = null;
T_CarTeamDao lDao = new T_CarTeamDao();
lDT = lDao.Select();
context.Response.Write(JsonConvert.SerializeObject(lDT));
}
private void SelectPage(HttpContext context)
{
DataTable lDT = null;
T_CarTeamDao lDao = new T_CarTeamDao();
int BeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
int EndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());
lDT = lDao.SelectPage(BeginIndex,EndIndex);
context.Response.Write(JsonConvert.SerializeObject(lDT));
}
private void Delete(HttpContext context)
{
string lID = context.Request.Params["ID"].ToString();
T_CarTeamDao lDao = new T_CarTeamDao();
if (lDao.Delete(lID) > 0)
{
    context.Response.Write("successed!");
}
else
{
    context.Response.Write("failed!");
}
}
private void Update(HttpContext context)
 {
T_CarTeamModel lModel = new T_CarTeamModel();
lModel.ID = context.Request.Params["ID"].ToString();
lModel.Name = context.Request.Params["Name"].ToString();
lModel.ID = Guid.NewGuid().ToString();
lModel.EditMan = "Admin";
lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
T_CarTeamDao lDao = new T_CarTeamDao();
if (lDao.Update(lModel) > 0)
{
    context.Response.Write("successed!");
}
else
{
    context.Response.Write("failed!");
}
 }
private void Insert(HttpContext context)
{
T_CarTeamModel lModel = new T_CarTeamModel();
lModel.ID = context.Request.Params["ID"].ToString();
lModel.Name = context.Request.Params["Name"].ToString();
lModel.ID = Guid.NewGuid().ToString();
lModel.EditMan = "Admin";
lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
T_CarTeamDao lDao = new T_CarTeamDao();
if (lDao.Insert(lModel) > 0)
{
    context.Response.Write("successed!");
}
else
{
    context.Response.Write("failed!");
}
}
 public bool IsReusable
{
get
{
 return false;
}
}
}
