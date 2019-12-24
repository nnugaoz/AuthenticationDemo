using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class HandlerGenerator
    {
        private TableInfo mTable = null;
        private String mHandlerPath = "Handler/";

        public HandlerGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GenerateHandlerClass()
        {
            if (mTable == null) return;

            if (!Directory.Exists(mHandlerPath))
            {
                Directory.CreateDirectory(mHandlerPath);
            }

            Generate_Ashx_File();

            Generate_Ashx_CS_File();
        }

        private void Generate_Ashx_CS_File()
        {

            FileStream lFileStream = File.Create(mHandlerPath + @"\" + mTable.Name + ".ashx.cs");
            String lLine = "";

            lLine = "using Newtonsoft.Json;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "using System;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "using System.Data;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "using System.Web;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "public class " + mTable.Name + " : IHttpHandler";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            Generate_ProcessRequest_Function(lFileStream);

            Generate_Select_Function(lFileStream);

            Generate_SelectByID_Function(lFileStream);

            Generate_SelectPage_Function(lFileStream);

            Generate_Delete_Function(lFileStream);

            Generate_Update_Function(lFileStream);

            Generate_Insert_Function(lFileStream);

            Generate_IsReusable_Property(lFileStream);

            lFileStream.Close();
        }

        private void Generate_IsReusable_Property(FileStream lFileStream)
        {
            String lLine = "";

            lLine = " public bool IsReusable";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "get";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " return false;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Insert_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "private void Insert(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Model lModel = new " + mTable.Name + "Model();";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "lModel." + mTable.Columns[i].Name + " = context.Request.Params[\"" + mTable.Columns[i].Name + "\"].ToString();";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "lModel.ID = Guid.NewGuid().ToString();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lModel.EditMan = \"Admin\";";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lModel.EditDate = DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss.fff\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Insert(lModel) > 0)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"1\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "else";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Update_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "private void Update(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Model lModel = new " + mTable.Name + "Model();";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "lModel." + mTable.Columns[i].Name + " = context.Request.Params[\"" + mTable.Columns[i].Name + "\"].ToString();";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "lModel.EditMan = \"Admin\";";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lModel.EditDate = DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss.fff\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Update(lModel) > 0)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"1\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "else";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " }";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Delete_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "private void Delete(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lID = context.Request.Params[\"ID\"].ToString();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Delete(lID) > 0)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"1\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "else";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectPage_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "private void SelectPage(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "int BeginIndex = Convert.ToInt32(context.Request.Params[\"BeginIndex\"].ToString());";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "int EndIndex = Convert.ToInt32(context.Request.Params[\"EndIndex\"].ToString());";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.SelectPage(BeginIndex,EndIndex);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "context.Response.Write(JsonConvert.SerializeObject(lDT));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectByID_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "private void SelectByID(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "String ID = context.Request.Params[\"ID\"].ToString();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.SelectByID(ID);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "context.Response.Write(JsonConvert.SerializeObject(lDT));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Select_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "private void Select(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "Dao lDao = new " + mTable.Name + "Dao();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.Select();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "context.Response.Write(JsonConvert.SerializeObject(lDT));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_ProcessRequest_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "public void ProcessRequest(HttpContext context)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lRequestMethod = context.Request.Params[\"RequestMethod\"].ToString();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "switch (lRequestMethod)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    case \"Insert\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "        Insert(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "        break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "   case \"Update\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "        Update(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    case \"Delete\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "        Delete(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "     case \"Select\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         Select(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "     case \"SelectPage\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         SelectPage(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "         break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "     case \"SelectByID\":";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    SelectByID(context);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    break;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Ashx_File()
        {
            String lLine = "";
            FileStream lFileStream = File.Create(mHandlerPath + @"\" + mTable.Name + ".ashx");
            lLine = "<%@ WebHandler Language = \"C#\" CodeBehind =\"" + mTable.Name + ".ashx.cs\" Class = \"" + mTable.Name + "\" %>";
            FileHelper.AppendLine(lFileStream, lLine);
            lFileStream.Close();
        }

    }
}
