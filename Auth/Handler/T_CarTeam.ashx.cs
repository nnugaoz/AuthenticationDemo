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
            case "SelectByID":
                SelectByID(context);
                break;
            case "Import":
                Import(context);
                break;
        }
    }

    private void Import(HttpContext context)
    {
        context.Response.Write("1");
    }
    private void Select(HttpContext context)
    {
        DataTable lDT = null;
        T_CarTeamDao lDao = new T_CarTeamDao();
        lDT = lDao.Select();
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void SelectByID(HttpContext context)
    {
        DataTable lDT = null;
        T_CarTeamDao lDao = new T_CarTeamDao();
        String ID = context.Request.Params["ID"].ToString();
        lDT = lDao.SelectByID(ID);
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void SelectPage(HttpContext context)
    {
        DataTable lDT = null;
        T_CarTeamDao lDao = new T_CarTeamDao();
        int BeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
        int EndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());
        string ID = context.Request.Params["ID"].ToString();
        string Name = context.Request.Params["Name"].ToString();
        string EditMan = context.Request.Params["EditMan"].ToString();
        string EditDate = context.Request.Params["EditDate"].ToString();
        lDT = lDao.SelectPage(ID, Name, EditMan, EditDate, BeginIndex, EndIndex);
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void Delete(HttpContext context)
    {
        string lID = context.Request.Params["ID"].ToString();
        T_CarTeamDao lDao = new T_CarTeamDao();
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
        T_CarTeamModel lModel = new T_CarTeamModel();
        lModel.ID = context.Request.Params["ID"].ToString();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.EditMan = context.Request.Params["EditMan"].ToString();
        lModel.EditDate = context.Request.Params["EditDate"].ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        T_CarTeamDao lDao = new T_CarTeamDao();
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
        T_CarTeamModel lModel = new T_CarTeamModel();
        lModel.ID = context.Request.Params["ID"].ToString();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.EditMan = context.Request.Params["EditMan"].ToString();
        lModel.EditDate = context.Request.Params["EditDate"].ToString();
        lModel.ID = Guid.NewGuid().ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        T_CarTeamDao lDao = new T_CarTeamDao();
        if (lDao.Insert(lModel) > 0)
        {
            context.Response.Write("1");
        }
        else
        {
            context.Response.Write("0");
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
