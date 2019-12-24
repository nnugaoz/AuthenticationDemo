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
        private string mPageProcedure = "Procedure/Page/";

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
            lLine += "lSQL += \" FROM " + lTableName + "\";\n";
            lLine += "lSQL += \" WHERE ID=@ID\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //SelectPage
            lLine = "public static string SelectPageSQL(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"EXEC " + lTableName + "_Page @BeginIndex,@EndIndex\";\n";
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

            lLine = "public DataTable SelectPage(int BeginIndex,int EndIndex)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectPageSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@BeginIndex\", BeginIndex));";
            AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@EndIndex\", EndIndex));";
            AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());";

            AppendLine(lFileStream, lLine);

            lLine = "return lDT;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "public DataTable SelectByID(string ID)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectByIDSQL();";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@ID\", ID));";
            AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());";

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

            lLine = "<script src='layer.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src = 'Pagination.js'></script>";
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

            lLine = "lPConfig.RequestUrl = '/Handler/" + lTableName + ".ashx?RequestMethod=SelectPage'";
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

            lLine = "function btnEdit_OnClick(event) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.open({";
            AppendLine(lFileStream, lLine);

            lLine = "type: 2";
            AppendLine(lFileStream, lLine);

            lLine = ", title: '编辑'";
            AppendLine(lFileStream, lLine);

            lLine = ", content: '" + lTableName + "Edit.html?ID=' + event.data.RowData.ID";
            AppendLine(lFileStream, lLine);

            lLine = ", area: ['800px', '600px']";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "function btnDel_OnClick(event) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.confirm('您确认要删除此条记录吗？', { icon: 3, title: '提示' }, function (index) {";
            AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            AppendLine(lFileStream, lLine);

            lLine = "type: 'get'";
            AppendLine(lFileStream, lLine);

            lLine = ", url: '/Handler/" + lTableName + ".ashx?RequestMethod=Delete&ID=' + event.data.RowData.ID";
            AppendLine(lFileStream, lLine);

            lLine = ", success: function (pData) {";
            AppendLine(lFileStream, lLine);

            lLine = "if (pData == '1') {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('删除成功！', { icon: 1 });";
            AppendLine(lFileStream, lLine);

            lLine = "Init();";
            AppendLine(lFileStream, lLine);

            lLine = "} else {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('删除失败！', { icon: 2 });";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "function btnNew_OnClick() {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.open({";
            AppendLine(lFileStream, lLine);

            lLine = "type: 2";
            AppendLine(lFileStream, lLine);

            lLine = ", title: '新增'";
            AppendLine(lFileStream, lLine);

            lLine = ", content: '" + lTableName + "New.html'";
            AppendLine(lFileStream, lLine);

            lLine = ", area: ['800px', '600px']";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = " </script>";
            AppendLine(lFileStream, lLine);

            lLine = "</head> ";
            AppendLine(lFileStream, lLine);

            lLine = "<body> ";
            AppendLine(lFileStream, lLine);

            lLine = "<input type='button' id='btnNew' class='btn btn-success' value='新增' onclick='btnNew_OnClick();'/>";
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

            lLine = "     case \"SelectPage\":";
            AppendLine(lFileStream, lLine);

            lLine = "         SelectPage(context);";
            AppendLine(lFileStream, lLine);

            lLine = "         break;";
            AppendLine(lFileStream, lLine);

            lLine = "     case \"SelectByID\":";
            AppendLine(lFileStream, lLine);

            lLine = "    SelectByID(context);";
            AppendLine(lFileStream, lLine);

            lLine = "    break;";
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

            lLine = "private void SelectByID(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "String ID = context.Request.Params[\"ID\"].ToString();";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.SelectByID(ID);";
            AppendLine(lFileStream, lLine);

            lLine = "context.Response.Write(JsonConvert.SerializeObject(lDT));";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "private void SelectPage(HttpContext context)";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            AppendLine(lFileStream, lLine);

            lLine = lTableName + "Dao lDao = new " + lTableName + "Dao();";
            AppendLine(lFileStream, lLine);

            lLine = "int BeginIndex = Convert.ToInt32(context.Request.Params[\"BeginIndex\"].ToString());";
            AppendLine(lFileStream, lLine);

            lLine = "int EndIndex = Convert.ToInt32(context.Request.Params[\"EndIndex\"].ToString());";
            AppendLine(lFileStream, lLine);

            lLine = "lDT = lDao.SelectPage(BeginIndex,EndIndex);";
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

            lLine = "    context.Response.Write(\"1\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
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

            lLine = "    context.Response.Write(\"1\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
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

            lLine = "    context.Response.Write(\"1\");";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "else";
            AppendLine(lFileStream, lLine);

            lLine = "{";
            AppendLine(lFileStream, lLine);

            lLine = "    context.Response.Write(\"0\");";
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

        private void btnCreateHtmlNewPage_Click(object sender, EventArgs e)
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

            lLine = "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>";
            AppendLine(lFileStream, lLine);

            lLine = "    <title></title>";
            AppendLine(lFileStream, lLine);

            lLine = "	<meta charset='utf-8' />";
            AppendLine(lFileStream, lLine);

            lLine = "<link href='bootstrap.min.css' rel='stylesheet'/>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='jquery-3.4.1.min.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='layer.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script type=\"text/javascript\">";
            AppendLine(lFileStream, lLine);

            lLine = "$(function () {";
            AppendLine(lFileStream, lLine);

            lLine = "$('#btnSubmit').on('click', btnSubmit_OnClick);";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "function btnSubmit_OnClick(event) {";
            AppendLine(lFileStream, lLine);

            lLine = "event.preventDefault();";
            AppendLine(lFileStream, lLine);

            lLine = "var formData = new FormData($('form')[0]);";
            AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            AppendLine(lFileStream, lLine);

            lLine = "type: 'post'";
            AppendLine(lFileStream, lLine);

            lLine = ", url: $('form').attr('action')";
            AppendLine(lFileStream, lLine);

            lLine = ", data: formData";
            AppendLine(lFileStream, lLine);

            lLine = ", processData: false";
            AppendLine(lFileStream, lLine);

            lLine = ", contentType: false";
            AppendLine(lFileStream, lLine);

            lLine = ", success: function (pData) {";
            AppendLine(lFileStream, lLine);

            lLine = "if (pData === '1') {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存成功!', { icon: 1 }, function (index) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            AppendLine(lFileStream, lLine);

            lLine = "index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引";
            AppendLine(lFileStream, lLine);

            lLine = "parent.layer.close(index); //再执行关闭";
            AppendLine(lFileStream, lLine);

            lLine = "parent.Init();";
            AppendLine(lFileStream, lLine);

            lLine = "})";
            AppendLine(lFileStream, lLine);

            lLine = "} else {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存失败!', { icon: 2 }, function (index) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            AppendLine(lFileStream, lLine);

            lLine = "})";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "return false;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "</script>";
            AppendLine(lFileStream, lLine);

            lLine = "</head>";
            AppendLine(lFileStream, lLine);

            lLine = "<body>";
            AppendLine(lFileStream, lLine);

            lLine = "<form method='POST' action='/Handler/" + lTableName + ".ashx?RequestMethod=Insert' class='form-horizontal'>";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "<div class='form-group'>";
                AppendLine(lFileStream, lLine);

                lLine = "<label for='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' class='col-sm-2 control-label'>" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "</label>";
                AppendLine(lFileStream, lLine);

                lLine = "<div class='col-sm-8'>";
                AppendLine(lFileStream, lLine);

                lLine = "<input type='text' id='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' name='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' class='form-control' />";
                AppendLine(lFileStream, lLine);

                lLine = "</div>";
                AppendLine(lFileStream, lLine);

                lLine = "</div>";
                AppendLine(lFileStream, lLine);

            }

            lLine = "<div class='form-group'>";
            AppendLine(lFileStream, lLine);

            lLine = "<div class='col-sm-offset-2 col-sm-8'>";
            AppendLine(lFileStream, lLine);

            lLine = "<input type='submit' value='提交' class='btn btn-success' id='btnSubmit'/>";
            AppendLine(lFileStream, lLine);

            lLine = "</div>";
            AppendLine(lFileStream, lLine);

            lLine = "</div>";
            AppendLine(lFileStream, lLine);

            lLine = "</form>";
            AppendLine(lFileStream, lLine);

            lLine = "</body>";
            AppendLine(lFileStream, lLine);

            lLine = "</html>";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void btnCreatePagePS_Click(object sender, EventArgs e)
        {
            String lTableName = lstTable.SelectedItem.ToString();
            if (!Directory.Exists(mPageProcedure + @"/"))
            {
                Directory.CreateDirectory(mPageProcedure + @"/");
            }

            FileStream lFileStream = new FileStream(mPageProcedure + @"/" + lTableName + "_Page.sql", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            String lLine = "";

            lLine = "IF OBJECT_ID('" + lTableName + "_Page') IS NOT NULL";
            AppendLine(lFileStream, lLine);

            lLine = "BEGIN ";
            AppendLine(lFileStream, lLine);

            lLine = "DROP PROCEDURE " + lTableName + "_Page ";
            AppendLine(lFileStream, lLine);

            lLine = "END ";
            AppendLine(lFileStream, lLine);

            lLine = "GO ";
            AppendLine(lFileStream, lLine);

            lLine = "CREATE PROCEDURE " + lTableName + "_Page ";
            AppendLine(lFileStream, lLine);

            lLine = "@BeginIndex INT";
            AppendLine(lFileStream, lLine);

            lLine = ", @EndIndex INT";
            AppendLine(lFileStream, lLine);

            lLine = "AS";
            AppendLine(lFileStream, lLine);

            lLine = "BEGIN";
            AppendLine(lFileStream, lLine);

            lLine = "SET NOCOUNT ON;";
            AppendLine(lFileStream, lLine);

            lLine = "DECLARE @RowCnt AS INT";
            AppendLine(lFileStream, lLine);

            lLine = "SELECT @RowCnt = COUNT(*) FROM " + lTableName;
            AppendLine(lFileStream, lLine);

            lLine = "SELECT * FROM(";
            AppendLine(lFileStream, lLine);

            lLine = "SELECT";
            AppendLine(lFileStream, lLine);

            lLine = "ROW_NUMBER()OVER(ORDER BY ID) RowIdx";
            AppendLine(lFileStream, lLine);

            lLine = ", @RowCnt RowCnt";
            AppendLine(lFileStream, lLine);

            lLine = " , *";
            AppendLine(lFileStream, lLine);

            lLine = "FROM " + lTableName + ")T";
            AppendLine(lFileStream, lLine);

            lLine = "WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex";
            AppendLine(lFileStream, lLine);

            lLine = "END";
            AppendLine(lFileStream, lLine);

            lLine = "GO ";
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

            FileStream lFileStream = new FileStream(mHtmlPath + @"\" + lTableName + @"\" + lTableName + "Edit.html", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            String lLine = "";

            lLine = "<!DOCTYPE html>";
            AppendLine(lFileStream, lLine);

            lLine = "<html>";
            AppendLine(lFileStream, lLine);

            lLine = "<head>";
            AppendLine(lFileStream, lLine);

            lLine = "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>";
            AppendLine(lFileStream, lLine);

            lLine = "    <title></title>";
            AppendLine(lFileStream, lLine);

            lLine = "	<meta charset='utf-8' />";
            AppendLine(lFileStream, lLine);

            lLine = "<link href='bootstrap.min.css' rel='stylesheet'/>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='jquery.min.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='layer.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script src='Common.js'></script>";
            AppendLine(lFileStream, lLine);

            lLine = "<script type='text/javascript'>";
            AppendLine(lFileStream, lLine);

            lLine = "$(function () {";
            AppendLine(lFileStream, lLine);

            lLine = "var ID = GetParameterByName('ID');";
            AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            AppendLine(lFileStream, lLine);

            lLine = "    type: 'get'";
            AppendLine(lFileStream, lLine);

            lLine = "     , url: '/Handler/" + lTableName + ".ashx?RequestMethod=SelectByID&ID=' + ID";
            AppendLine(lFileStream, lLine);

            lLine = " , success: function (pData) { PageInit(pData); }";
            AppendLine(lFileStream, lLine);

            lLine = "   });";
            AppendLine(lFileStream, lLine);

            lLine = "$('#btnSubmit').on('click', btnSubmit_OnClick);";
            AppendLine(lFileStream, lLine);

            lLine = " });";
            AppendLine(lFileStream, lLine);

            lLine = "function PageInit(pData) {";
            AppendLine(lFileStream, lLine);

            lLine = "    var lData = JSON.parse(pData);";
            AppendLine(lFileStream, lLine);

            for (var i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "$('#" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "').val(lData[0]." + dgvFields.Rows[i].Cells["Name"].Value.ToString() + ");";
                AppendLine(lFileStream, lLine);
            }

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "function btnSubmit_OnClick(event) {";
            AppendLine(lFileStream, lLine);

            lLine = "event.preventDefault();";
            AppendLine(lFileStream, lLine);

            lLine = "var formData = new FormData($('form')[0]);";
            AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            AppendLine(lFileStream, lLine);

            lLine = "type: 'post'";
            AppendLine(lFileStream, lLine);

            lLine = ", url: $('form').attr('action')";
            AppendLine(lFileStream, lLine);

            lLine = ", contentType: false";
            AppendLine(lFileStream, lLine);

            lLine = ", processData: false";
            AppendLine(lFileStream, lLine);

            lLine = ", data: formData";
            AppendLine(lFileStream, lLine);

            lLine = ", success: function (pData) {";
            AppendLine(lFileStream, lLine);

            lLine = "if (pData === '1') {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存成功！'";
            AppendLine(lFileStream, lLine);

            lLine = ", { icon: 1 }";
            AppendLine(lFileStream, lLine);

            lLine = ", function (index) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            AppendLine(lFileStream, lLine);

            lLine = "index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引";
            AppendLine(lFileStream, lLine);

            lLine = "parent.layer.close(index); //再执行关闭";
            AppendLine(lFileStream, lLine);

            lLine = "parent.Init();";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "} else {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存失败！'";
            AppendLine(lFileStream, lLine);

            lLine = ", { icon: 2 }";
            AppendLine(lFileStream, lLine);

            lLine = ", function (index) {";
            AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "});";
            AppendLine(lFileStream, lLine);

            lLine = "return false;";
            AppendLine(lFileStream, lLine);

            lLine = "}";
            AppendLine(lFileStream, lLine);

            lLine = "</script>";
            AppendLine(lFileStream, lLine);

            lLine = "</head>";
            AppendLine(lFileStream, lLine);

            lLine = "<body>";
            AppendLine(lFileStream, lLine);

            lLine = "<form method='POST' action='/Handler/" + lTableName + ".ashx?RequestMethod=Update' class='form-horizontal'>";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "<div class='form-group'>";
                AppendLine(lFileStream, lLine);

                lLine = "<label for='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' class='col-sm-2 control-label'>" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "</label>";
                AppendLine(lFileStream, lLine);

                lLine = "<div class='col-sm-8'>";
                AppendLine(lFileStream, lLine);

                lLine = "<input type='text' id='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' name='" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "' class='form-control' />";
                AppendLine(lFileStream, lLine);

                lLine = "</div>";
                AppendLine(lFileStream, lLine);

                lLine = "</div>";
                AppendLine(lFileStream, lLine);
            }

            lLine = "<div class='form-group'>";
            AppendLine(lFileStream, lLine);

            lLine = "<div class='col-sm-offset-2 col-sm-8'>";
            AppendLine(lFileStream, lLine);

            lLine = "<input type='submit' value='提交' class='btn btn-success' id='btnSubmit'/>";
            AppendLine(lFileStream, lLine);

            lLine = "</div>";
            AppendLine(lFileStream, lLine);

            lLine = "</div>";
            AppendLine(lFileStream, lLine);

            lLine = "</form>";
            AppendLine(lFileStream, lLine);

            lLine = "</body>";
            AppendLine(lFileStream, lLine);

            lLine = "</html>";
            AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void btnOneKey_Click(object sender, EventArgs e)
        {
            btnGenerateEntityClass_Click(sender, e);
            btnGenerateDao_Click(sender, e);
            btnCreateHtmlListPage_Click(sender, e);
            btnCreateHtmlNewPage_Click(sender, e);
            btnCreateHtmlEditPage_Click(sender, e);
            btnCreateHandler_Click(sender, e);
            btnCreatePagePS_Click(sender, e);
        }
    }
}
