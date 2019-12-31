using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class Role_Menu : IHttpHandler
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
     case "SelectByID":
    SelectByID(context);
    break;
     case "Import":
    Import(context);
    break;
     case "Export":
    Export(context);
    break;
}
}
private void Select(HttpContext context)
{
DataTable lDT = null;
Role_MenuDao lDao = new Role_MenuDao();
lDT = lDao.Select();
context.Response.Write(JsonConvert.SerializeObject(lDT));
}
private void SelectByID(HttpContext context)
{
DataTable lDT = null;
Role_MenuDao lDao = new Role_MenuDao();
String ID = context.Request.Params["ID"].ToString();
lDT = lDao.SelectByID(ID);
context.Response.Write(JsonConvert.SerializeObject(lDT));
}
private void SelectPage(HttpContext context)
{
DataTable lDT = null;
Role_MenuDao lDao = new Role_MenuDao();
int BeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
int EndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());
string RID = context.Request.Params["RID"].ToString();
string MID = context.Request.Params["MID"].ToString();
lDT = lDao.SelectPage(RID, MID, BeginIndex, EndIndex);
context.Response.Write(JsonConvert.SerializeObject(lDT));
}
private void Delete(HttpContext context)
{
string lID = context.Request.Params["ID"].ToString();
Role_MenuDao lDao = new Role_MenuDao();
if (lDao.Delete(lID) > 0)
{
    context.Response.Write("1");
}
else
{
    context.Response.Write("0");
}
}
private void Update(HttpContext context)
 {
Role_MenuModel lModel = new Role_MenuModel();
lModel.ID = context.Request.Params["ID"].ToString();
lModel.RID = context.Request.Params["RID"].ToString();
lModel.MID = context.Request.Params["MID"].ToString();
Role_MenuDao lDao = new Role_MenuDao();
if (lDao.Update(lModel) > 0)
{
    context.Response.Write("1");
}
else
{
    context.Response.Write("0");
}
 }
private void Insert(HttpContext context)
{
Role_MenuModel lModel = new Role_MenuModel();
lModel.ID = context.Request.Params["ID"].ToString();
lModel.RID = context.Request.Params["RID"].ToString();
lModel.MID = context.Request.Params["MID"].ToString();
lModel.ID = Guid.NewGuid().ToString();
Role_MenuDao lDao = new Role_MenuDao();
if (lDao.Insert(lModel) > 0)
{
    context.Response.Write("1");
}
else
{
    context.Response.Write("0");
}
}
private void Import(HttpContext context)
{
if (context.Request.Files.Count > 0)
{
if (context.Request.Files[0].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
{
string lFileSept = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") ;
string lFileName = @"E:\00_Temp\" + lFileSept + context.Request.Files[0].FileName;
context.Request.Files[0].SaveAs(lFileName);
Role_MenuDao lDao = new Role_MenuDao();
if (lDao.Import(lFileName) == 1)
{
context.Response.Write('1');
return;
}
else
{
context.Response.Write('0');
return;
}
}
else
 {
context.Response.Write('0');
return;
}
}
}
private void Export(HttpContext context)
{
RequestResult lRR = new RequestResult();
String lExcelFilePath = "";
Role_MenuDao lDao = new Role_MenuDao();
lDao.Export(ref lExcelFilePath);
lRR.Msg = @"\TempFile\" + lExcelFilePath;
context.Response.Write(JsonConvert.SerializeObject(lRR));
 }
 public bool IsReusable
{
get
{
 return false;
}
}
}
