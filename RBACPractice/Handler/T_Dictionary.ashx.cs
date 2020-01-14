using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
public class T_Dictionary : IHttpHandler
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
        T_DictionaryDao lDao = new T_DictionaryDao();
        lDT = lDao.Select();
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void SelectByID(HttpContext context)
    {
        DataTable lDT = null;
        T_DictionaryDao lDao = new T_DictionaryDao();
        String ID = context.Request.Params["ID"].ToString();
        lDT = lDao.SelectByID(ID);
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void SelectPage(HttpContext context)
    {
        DataTable lDT = null;
        T_DictionaryDao lDao = new T_DictionaryDao();
        int BeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
        int EndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());
        string Name = context.Request.Params["Name"].ToString();
        string DTID = context.Request.Params["DTID"].ToString();
        lDT = lDao.SelectPage(Name, DTID, BeginIndex, EndIndex);
        context.Response.Write(JsonConvert.SerializeObject(lDT));
    }
    private void Delete(HttpContext context)
    {
        string lID = context.Request.Params["ID"].ToString();
        T_DictionaryDao lDao = new T_DictionaryDao();
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
        T_DictionaryModel lModel = new T_DictionaryModel();
        lModel.ID = context.Request.Params["ID"].ToString();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.DTID = context.Request.Params["DTID"].ToString();
        lModel.EditMan = context.Request.Params["EditMan"].ToString();
        lModel.EditDate = context.Request.Params["EditDate"].ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        T_DictionaryDao lDao = new T_DictionaryDao();
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
        T_DictionaryModel lModel = new T_DictionaryModel();
        lModel.Name = context.Request.Params["Name"].ToString();
        lModel.DTID = context.Request.Params["cmbDicType"].ToString();
        lModel.ID = Guid.NewGuid().ToString();
        lModel.EditMan = "Admin";
        lModel.EditDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        T_DictionaryDao lDao = new T_DictionaryDao();
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
                T_DictionaryDao lDao = new T_DictionaryDao();
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
        T_DictionaryDao lDao = new T_DictionaryDao();
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
