using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
public class T_Role : IHttpHandler
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
        T_RoleDao lDao = new T_RoleDao();
        lDT = lDao.Select();
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void SelectByID(HttpContext context)
    {
        DataSet lDS = new DataSet();

        DataTable lDTRoleData = null;
        T_RoleDao lRoleDao = new T_RoleDao();
        String ID = context.Request.Params["ID"].ToString();
        lDTRoleData = lRoleDao.SelectByID(ID);
        lDTRoleData.TableName = "RoleData";
        lDS.Tables.Add(lDTRoleData.Copy());

        DataTable lDTMenuData = null;
        T_MenuDao lMenuDao = new T_MenuDao();
        lDTMenuData = lMenuDao.Select();
        lDTMenuData.TableName = "MenuData";
        lDS.Tables.Add(lDTMenuData.Copy());

        context.Response.Write(JsonConvert.SerializeObject(lDS));
    }
    private void SelectPage(HttpContext context)
    {
        DataTable lDT = null;
        T_RoleDao lDao = new T_RoleDao();
        int BeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
        int EndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());
        string Name = context.Request.Params["Name"].ToString();
        lDT = lDao.SelectPage(Name, BeginIndex, EndIndex);
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void Delete(HttpContext context)
    {
        string lID = context.Request.Params["ID"].ToString();
        T_RoleDao lDao = new T_RoleDao();
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
        V_RoleModel lModel = new V_RoleModel();
        lModel.RoleMenuList = new List<T_Role_MenuModel>();

        lModel.ID = context.Request.Params["ID"].ToString();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.EditMan = context.Request.Params["EditMan"].ToString();
        lModel.EditDate = context.Request.Params["EditDate"].ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        string[] lMenuArr = context.Request.Params["Menu"].ToString().Split(new char[] { ',' });
        T_Role_MenuModel lRoleMenuModel = null;

        for (int i = 0; i < lMenuArr.Length; i++)
        {
            lRoleMenuModel = new T_Role_MenuModel();
            lRoleMenuModel.ID = Guid.NewGuid().ToString();
            lRoleMenuModel.RID = lModel.ID;
            lRoleMenuModel.MID = lMenuArr[i];
            lRoleMenuModel.EditMan = "Admin";
            lRoleMenuModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            lModel.RoleMenuList.Add(lRoleMenuModel);
        }

        T_RoleDao lDao = new T_RoleDao();
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
        V_RoleModel lModel = new V_RoleModel();
        lModel.RoleMenuList = new List<T_Role_MenuModel>();

        lModel.ID = context.Request.Params["ID"].ToString();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.EditMan = context.Request.Params["EditMan"].ToString();
        lModel.EditDate = context.Request.Params["EditDate"].ToString();
        lModel.ID = Guid.NewGuid().ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        string[] lMenus = context.Request.Params["Menu"].ToString().Split(new char[] { ',' });
        T_Role_MenuModel lRoleMenuModel = null;

        for (int i = 0; i < lMenus.Length; i++)
        {
            lRoleMenuModel = new T_Role_MenuModel();
            lRoleMenuModel.ID = Guid.NewGuid().ToString();
            lRoleMenuModel.RID = lModel.ID;
            lRoleMenuModel.MID = lMenus[i];
            lRoleMenuModel.EditMan = "Admin";
            lRoleMenuModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            lModel.RoleMenuList.Add(lRoleMenuModel);
        }

        T_RoleDao lDao = new T_RoleDao();
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
                string lFileSept = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
                string lFileName = @"E:\00_Temp\" + lFileSept + context.Request.Files[0].FileName;
                context.Request.Files[0].SaveAs(lFileName);
                T_RoleDao lDao = new T_RoleDao();
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
        T_RoleDao lDao = new T_RoleDao();
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
