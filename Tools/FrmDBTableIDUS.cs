using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tools
{
    public partial class FrmDBTableIDUS : Form
    {

        private string mModelPath = "Model/";
        private string mDaoPath = "Dao/";
        private string mHtmlPath = "Html/";
        private string mHandlerPath = "Handler/";

        public FrmDBTableIDUS()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtConnectionString.Text.Trim() == "") return;
            DataTable lDT = new DataTable();

            using (SqlConnection lConnection = new SqlConnection(txtConnectionString.Text))
            {
                lConnection.Open();
                string lSQL = "SELECT name FROM sys.databases ORDER BY name";
                SqlCommand lSqlCommand = new SqlCommand(lSQL, lConnection);
                SqlDataAdapter lSqlDataAdapter = new SqlDataAdapter(lSqlCommand);
                lSqlDataAdapter.Fill(lDT);
            }

            lstDB.Items.Clear();
            for (int i = 0; i < lDT.Rows.Count; i++)
            {
                lstDB.Items.Add(lDT.Rows[i][0].ToString());
            }
        }

        private void lstDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtConnectionString.Text.Trim() == "") return;
            DataTable lDT = new DataTable();
            String lDataBaseName = lstDB.SelectedItem.ToString();

            using (SqlConnection lConnection = new SqlConnection(txtConnectionString.Text))
            {
                lConnection.Open();
                string lSQL = "";
                lSQL += "USE " + lDataBaseName + ";";
                lSQL += "SELECT name FROM sys.tables";
                SqlCommand lSqlCommand = new SqlCommand(lSQL, lConnection);
                SqlDataAdapter lSqlDataAdapter = new SqlDataAdapter(lSqlCommand);
                lSqlDataAdapter.Fill(lDT);
            }

            lstTable.Items.Clear();
            for (int i = 0; i < lDT.Rows.Count; i++)
            {
                lstTable.Items.Add(lDT.Rows[i][0].ToString());
            }
        }

        private void lstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtConnectionString.Text.Trim() == "") return;
            DataTable lDT = new DataTable();
            String lDataBaseName = lstDB.SelectedItem.ToString();
            String lTableName = lstTable.SelectedItem.ToString();

            using (SqlConnection lConnection = new SqlConnection(txtConnectionString.Text))
            {
                lConnection.Open();
                string lSQL = "";
                lSQL += "USE " + lDataBaseName + ";";
                lSQL += " SELECT syscol.name AS Name  ";
                lSQL += ", systype.name AS Type";
                lSQL += ", syscol.max_length AS Length";
                lSQL += " FROM";
                lSQL += " sys.tables systab left join sys.columns syscol on systab.object_id=syscol.object_id";
                lSQL += " left join sys.types systype on syscol.system_type_id=systype.system_type_id";
                lSQL += " WHERE systab.name = '" + lTableName + "'";
                lSQL += " ORDER BY syscol.column_id";

                SqlCommand lSqlCommand = new SqlCommand(lSQL, lConnection);
                SqlDataAdapter lSqlDataAdapter = new SqlDataAdapter(lSqlCommand);
                lSqlDataAdapter.Fill(lDT);
            }

            dgvFields.DataSource = lDT;
        }

        private void btnGenerateEntityClass_Click(object sender, EventArgs e)
        {
            if (lstTable.Items.Count == 0)
            {
                MessageBox.Show("请先连接数据库!", "系统", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String lTableName = lstTable.SelectedItem.ToString();
            String lEntityClassFileName = lTableName + "Model.cs";
            String lFieldName = "";
            String lFieldType = "";
            int lFieldLen = 0;
            string lLine = "";

            if (!Directory.Exists(mModelPath))
            {
                Directory.CreateDirectory(mModelPath);
            }
            FileStream lFileStream = File.Create(mModelPath + @"\" + lEntityClassFileName);

            //类的声明
            lLine = "public class " + lTableName + "Model{";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lFieldLen = Convert.ToInt32(dgvFields.Rows[i].Cells["Length"].Value.ToString());
                lLine = "//MaxLength=" + lFieldLen.ToString();
                AppendLine(lFileStream, lLine);

                lLine = "public ";
                lFieldType = dgvFields.Rows[i].Cells["Type"].Value.ToString();
                switch (lFieldType)
                {
                    case "varchar":
                        lLine += "string ";
                        break;

                    case "decimal":
                        lLine += "decimal ";
                        break;

                    default:
                        lLine += "string ";
                        break;
                }
                lFieldName = dgvFields.Rows[i].Cells["Name"].Value.ToString();

                //字段声明
                lLine += lFieldName + "{get;set;}";
                AppendLine(lFileStream, lLine);
            }

            //类声明结束
            lLine = "}";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void AppendLine(FileStream pFs, String pLine)
        {
            byte[] lData = Encoding.UTF8.GetBytes(pLine + "\n");
            pFs.Write(lData, 0, lData.Length);
        }

        private void btnGenerateDao_Click(object sender, EventArgs e)
        {
            string lTableName = lstTable.SelectedItem.ToString();
            String lDaoFileName = lTableName + "Dao.cs";
            String lLine = "";

            if (!Directory.Exists(mDaoPath))
            {
                Directory.CreateDirectory(mDaoPath);
            }

            FileStream lFileStream = File.Create(mDaoPath + @"\" + lDaoFileName);

            lLine = "using System.Collections.Generic;";
            AppendLine(lFileStream, lLine);

            lLine = "using System.Data.SqlClient;";
            AppendLine(lFileStream, lLine);

            lLine = "using System.Data;";
            AppendLine(lFileStream, lLine);

            lLine = "public class " + lTableName + "Dao{";
            AppendLine(lFileStream, lLine);

            //INSERT
            lLine = "public static string InsertSQL(){";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"INSERT INTO " + lTableName + "(\";\n";
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
                else
                {
                    lLine += "lSQL += \"," + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
            }
            lLine += "lSQL += \")VALUES(\";\n";
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
                else
                {
                    lLine += "lSQL += \",@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
            }
            lLine += "lSQL += \")\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //Delete
            lLine = "public static string DeleteSQL(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"DELETE FROM " + lTableName + " \";\n";
            lLine += "lSQL += \" WHERE ID=@ID\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //UPDATE
            lLine = "public static string UpdateSQL(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"UPDATE " + lTableName + " SET \";\n";
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "=@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
                else
                {
                    lLine += "lSQL += \"," + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "=@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
            }
            lLine += "lSQL += \" WHERE ID=@ID\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //SELECT
            lLine = "public static string SelectSQL(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"SELECT \";\n";
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
                else
                {
                    lLine += "lSQL += \"," + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
            }
            lLine += "lSQL += \" FROM " + lTableName + "\";\n";

            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //SELECTByID
            lLine = "public static string SelectByIDSQL(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"SELECT \";\n";
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
                else
                {
                    lLine += "lSQL += \"," + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\";\n";
                }
            }
            lLine += "lSQL += \"FROM " + lTableName + "\";\n";

            lLine += "lSQL += \")\";\n";
            lLine += "lSQL += \"WHERE ID=@ID\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            lLine = "public int Insert(" + lTableName + "Model " + lTableName + "Model)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = InsertSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "lParams.Add(new SqlParameter(\"@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\", " + lTableName + "Model." + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "));";
                AppendLine(lFileStream, lLine);
            }

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "public int Update(" + lTableName + "Model " + lTableName + "Model)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = UpdateSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "lParams.Add(new SqlParameter(\"@" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\", " + lTableName + "Model." + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "));";
                AppendLine(lFileStream, lLine);
            }

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "public int Delete(string pID)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = DeleteSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@ID\", pID));";
            AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "public DataTable Select()";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL);";
            AppendLine(lFileStream, lLine);

            lLine = "return lDT;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void btnCreateHtmlListPage_Click(object sender, EventArgs e)
        {
            string lTableName = lstTable.SelectedItem.ToString();

            if (!Directory.Exists(mHtmlPath + @"/" + lTableName))
            {
                Directory.CreateDirectory(mHtmlPath + @"/" + lTableName);
            }

            FileStream lFileStream = File.Create(mHtmlPath + @"/" + lTableName + @"/" + lTableName + "List.html");
            String lLine = "";
            lLine = "<!DOCTYPE html>";
            AppendLine(lFileStream, lLine);

            lLine = "<html xmlns=\"http://www.w3.org/1999/xhtml\"> ";
            AppendLine(lFileStream, lLine);

            lLine = "<head > ";
            AppendLine(lFileStream, lLine);

            lLine = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
            AppendLine(lFileStream, lLine);

            lLine = "<title>" + lTableName + "列表 </title>";
            AppendLine(lFileStream, lLine);

            lLine = "<link href='bootstrap.min.css' rel='stylesheet'/>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='jquery-3.4.1.min.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script type='text/javascript'>";
            AppendLine(lFileStream, lLine);

            lLine = "$(function () {";
            AppendLine(lFileStream, lLine);

            lLine = "Init();";
            AppendLine(lFileStream, lLine);

            lLine = " })";
            AppendLine(lFileStream, lLine);

            lLine = "function Init() {";
            AppendLine(lFileStream, lLine);

            lLine = "$('#" + lTableName + "List').empty();";
            AppendLine(lFileStream, lLine);

            lLine = "var lPConfig = new PaginationConfiguration();";
            AppendLine(lFileStream, lLine);

            lLine = "lPConfig.RequestUrl = '/Handler/" + lTableName + ".ashx?RequestMethod=GET_LIST_PAGINATION'";
            AppendLine(lFileStream, lLine);

            lLine = "lPConfig.PageContainerID = '" + lTableName + "List';";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "var " + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "Col = new PaginationColumnConfiguration();";
                AppendLine(lFileStream, lLine);

                lLine = dgvFields.Rows[i].Cells["Name"].Value.ToString() + "Col.HeaderText = '" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "';";
                AppendLine(lFileStream, lLine);

                lLine = dgvFields.Rows[i].Cells["Name"].Value.ToString() + "Col.Type = 'TXT';";
                AppendLine(lFileStream, lLine);

                lLine = dgvFields.Rows[i].Cells["Name"].Value.ToString() + "Col.DataField = '" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "';";
                AppendLine(lFileStream, lLine);

                lLine = "lPConfig.Columns.push(" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "Col);";
                AppendLine(lFileStream, lLine);
            }

            lLine = "var EditCol = new PaginationColumnConfiguration();";
            AppendLine(lFileStream, lLine);

            lLine = "EditCol.HeaderText = '编辑';";
            AppendLine(lFileStream, lLine);

            lLine = "EditCol.Type = 'BTN';";
            AppendLine(lFileStream, lLine);

            lLine = "EditCol.DataField = '编辑';";
            AppendLine(lFileStream, lLine);

            lLine = "EditCol.CssClass = 'btn btn-success';";
            AppendLine(lFileStream, lLine);

            lLine = "EditCol.OnClickEventHandler = btnEdit_OnClick;";
            AppendLine(lFileStream, lLine);

            lLine = "lPConfig.Columns.push(EditCol);";
            AppendLine(lFileStream, lLine);

            lLine = "var DelCol = new PaginationColumnConfiguration();";
            AppendLine(lFileStream, lLine);

            lLine = "DelCol.HeaderText = '删除';";
            AppendLine(lFileStream, lLine);

            lLine = "DelCol.Type = 'BTN';";
            AppendLine(lFileStream, lLine);

            lLine = "DelCol.DataField = '删除';";
            AppendLine(lFileStream, lLine);

            lLine = "DelCol.CssClass = 'btn btn-success';";
            AppendLine(lFileStream, lLine);

            lLine = "DelCol.OnClickEventHandler = btnDel_OnClick;";
            AppendLine(lFileStream, lLine);

            lLine = "lPConfig.Columns.push(DelCol);";
            AppendLine(lFileStream, lLine);

            lLine = "var " + lTableName + "ListPagination = new Pagination(lPConfig);";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "ListPagination.RequestData(1);";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "function btnEdit_OnClick(){}";
            AppendLine(lFileStream, lLine);

            lLine = "function btnDel_OnClick(){}";
            AppendLine(lFileStream, lLine);

            lLine = " </script>";
            AppendLine(lFileStream, lLine);

            lLine = "</head> ";
            AppendLine(lFileStream, lLine);

            lLine = "<body> ";
            AppendLine(lFileStream, lLine);

            lLine = "<input type='button' id='btnNew' class='btn btn-success' value='新增' />";
            AppendLine(lFileStream, lLine);

            lLine = "<div id='" + lTableName + "List' style='margin-top:20px;'></div>";
            AppendLine(lFileStream, lLine);

            lLine = "</body> ";
            AppendLine(lFileStream, lLine);

            lLine = "</html> ";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void btnCreateHandler_Click(object sender, EventArgs e)
        {
            string lTableName = lstTable.SelectedItem.ToString();

            if (!Directory.Exists(mHandlerPath))
            {
                Directory.CreateDirectory(mHandlerPath);
            }

            FileStream lFileStream = File.Create(mHandlerPath + @"\" + lTableName + ".ashx");

            String lLine = "";

            lLine = "<%@ WebHandler Language = \"C#\" CodeBehind =\"" + lTableName + ".ashx.cs\" Class = \"" + lTableName + "\" %>";
            AppendLine(lFileStream, lLine);
            lFileStream.Close();

            lFileStream = File.Create(mHandlerPath + @"\" + lTableName + ".ashx.cs");

            lLine = "using Newtonsoft.Json;";
            AppendLine(lFileStream, lLine);

            lLine = "using System;";
            AppendLine(lFileStream, lLine);

            lLine = "using System.Data;";
            AppendLine(lFileStream, lLine);

            lLine = "using System.Web;";
            AppendLine(lFileStream, lLine);

            lLine = "public class " + lTableName + " : IHttpHandler";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "public void ProcessRequest(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lRequestMethod = context.Request.Params[\"RequestMethod\"].ToString();";
            AppendLine(lFileStream, lLine);

            lLine = "switch (lRequestMethod)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    case \"Insert\":";
            AppendLine(lFileStream, lLine);

            lLine = "        Insert(context);";
            AppendLine(lFileStream, lLine);

            lLine = "        break;";
            AppendLine(lFileStream, lLine);

            lLine = "   case \"Update\":";
            AppendLine(lFileStream, lLine);

            lLine = "        Update(context);";
            AppendLine(lFileStream, lLine);

            lLine = "         break;";
            AppendLine(lFileStream, lLine);

            lLine = "    case \"Delete\":";
            AppendLine(lFileStream, lLine);

            lLine = "        Delete(context);";
            AppendLine(lFileStream, lLine);

            lLine = "         break;";
            AppendLine(lFileStream, lLine);

            lLine = "     case \"Select\":";
            AppendLine(lFileStream, lLine);

            lLine = "         Select(context);";
            AppendLine(lFileStream, lLine);

            lLine = "         break;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "private void Select(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.Select();";
            AppendLine(lFileStream, lLine);

            lLine = "context.Response.Write(JsonConvert.SerializeObject(lDT));";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "private void Delete(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lID = context.Request.Params[\"ID\"].ToString();";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Delete(lID) > 0)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"successed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"failed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "private void Update(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = " {";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Model lModel = new " + lTableName + "Model();";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "lModel." + dgvFields.Rows[i].Cells["Name"].Value.ToString() + " = context.Request.Params[\"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\"].ToString();";
                AppendLine(lFileStream, lLine);
            }

            lLine = "lModel.ID = Guid.NewGuid().ToString();";
            AppendLine(lFileStream, lLine);

            lLine = "lModel.EditMan = \"Admin\";";
            AppendLine(lFileStream, lLine);

            lLine = "lModel.EditDate = DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss.fff\");";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Update(lModel) > 0)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"successed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"failed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = " }";
            AppendLine(lFileStream, lLine);

            lLine = "private void Insert(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Model lModel = new " + lTableName + "Model();";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "lModel." + dgvFields.Rows[i].Cells["Name"].Value.ToString() + " = context.Request.Params[\"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\"].ToString();";
                AppendLine(lFileStream, lLine);
            }

            lLine = "lModel.ID = Guid.NewGuid().ToString();";
            AppendLine(lFileStream, lLine);

            lLine = "lModel.EditMan = \"Admin\";";
            AppendLine(lFileStream, lLine);

            lLine = "lModel.EditDate = DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss.fff\");";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "if (lDao.Insert(lModel) > 0)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"successed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"failed!\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = " public bool IsReusable";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "get";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = " return false;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void btnCreateHtmlEditPage_Click(object sender, EventArgs e)
        {
            string lTableName = lstTable.SelectedItem.ToString();
            if (!Directory.Exists(mHtmlPath + @"\" + lTableName))
            {
                Directory.CreateDirectory(mHtmlPath + @"\" + lTableName);
            }

            FileStream lFileStream = new FileStream(mHtmlPath + @"\" + lTableName + @"\" + lTableName + "New.html", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            String lLine = "";

            lLine = "<!DOCTYPE html>";
            AppendLine(lFileStream, lLine);

            lLine = "<html>";
            AppendLine(lFileStream, lLine);

            lLine = "<head>";
            AppendLine(lFileStream, lLine);

            lLine = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>";
            AppendLine(lFileStream, lLine);

            lLine = "    <title></title>";
            AppendLine(lFileStream, lLine);

            lLine = "	<meta charset=\"utf-8\" />";
            AppendLine(lFileStream, lLine);

            lLine = "</head>";
            AppendLine(lFileStream, lLine);

            lLine = "<body>";
            AppendLine(lFileStream, lLine);

            lLine = "<form method=\"POST\" action=\"/Handler/" + lTableName + ".ashx?RequestMethod=Insert\">";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "<label for=\"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\">" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "</label>";
                AppendLine(lFileStream, lLine);
                lLine = "<input type=\"text\" id=\"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\" name=\"" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "\"/>";
                AppendLine(lFileStream, lLine);
                lLine = "<br/>";
                AppendLine(lFileStream, lLine);
            }

            lLine = "<input type=\"submit\" value=\"提交\" />";
            AppendLine(lFileStream, lLine);

            lLine = "</form>";
            AppendLine(lFileStream, lLine);

            lLine = "</body>";
            AppendLine(lFileStream, lLine);

            lLine = "</html>";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }
    }
}
