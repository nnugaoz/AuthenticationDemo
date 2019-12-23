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

            lLine = "public class " + lTableName + "Dao{";
            AppendLine(lFileStream, lLine);

            //INSERT
            lLine = "public string Insert(){";
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
            lLine = "public string Delete(){\n";
            lLine += "string lSQL=\"\";\n";
            lLine += "lSQL += \"DELETE FROM " + lTableName + " \";\n";
            lLine += "lSQL += \" WHERE ID=@ID\";\n";
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //UPDATE
            lLine = "public string Update(){\n";
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
            lLine = "public string Select(){\n";
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
            lLine += "return lSQL;\n";
            lLine += "}";
            AppendLine(lFileStream, lLine);

            //SELECTByID
            lLine = "public string SelectByID(){\n";
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

            lLine = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset = utf-8\" />";
            AppendLine(lFileStream, lLine);

            lLine = "<title>" + lTableName + "列表 </title>";
            AppendLine(lFileStream, lLine);

            lLine = "<script type=\"text/javascript\" >";
            AppendLine(lFileStream, lLine);

            lLine = "</script>";
            AppendLine(lFileStream, lLine);

            lLine = "</head> ";
            AppendLine(lFileStream, lLine);

            lLine = "<body> ";
            AppendLine(lFileStream, lLine);

            lLine = "<table class=\"table table-bordered table - striped table - hover\">";
            AppendLine(lFileStream, lLine);

            lLine = "<tr>";
            AppendLine(lFileStream, lLine);

            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lLine = "<th>" + dgvFields.Rows[i].Cells["Name"].Value.ToString() + "</th>";
                AppendLine(lFileStream, lLine);
            }
            lLine = "</tr>";
            AppendLine(lFileStream, lLine);

            lLine = "</table>";
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
    }
}
