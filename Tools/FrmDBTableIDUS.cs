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
                lSQL += ", cast(tabcmt.value as varchar) AS Comment";
                lSQL += " FROM";
                lSQL += " sys.tables systab left join sys.columns syscol on systab.object_id=syscol.object_id";
                lSQL += " left join sys.types systype on syscol.system_type_id=systype.system_type_id";
                lSQL += " left join fn_listextendedproperty ('MS_DESCRIPTION','schema', 'dbo', 'table', '" + lTableName + "', 'column', null) tabcmt ON tabcmt.objname=syscol.name COLLATE Chinese_PRC_CI_AS";
                lSQL += " WHERE systab.name = '" + lTableName + "'";
                lSQL += " ORDER BY syscol.column_id";

                SqlCommand lSqlCommand = new SqlCommand(lSQL, lConnection);
                SqlDataAdapter lSqlDataAdapter = new SqlDataAdapter(lSqlCommand);
                lSqlDataAdapter.Fill(lDT);
            }

            dgvFields.DataSource = lDT;

            if (dgvFields.Rows.Count > 0)
            {
                DataGridViewCheckBoxColumn lListVisibleCol = new DataGridViewCheckBoxColumn();
                lListVisibleCol.Name = "ListVisible";
                lListVisibleCol.DefaultCellStyle.NullValue = true;
                dgvFields.Columns.Add(lListVisibleCol);

                DataGridViewCheckBoxColumn lNewVisibleCol = new DataGridViewCheckBoxColumn();
                lNewVisibleCol.Name = "NewVisible";
                lNewVisibleCol.DefaultCellStyle.NullValue = true;
                dgvFields.Columns.Add(lNewVisibleCol);

                DataGridViewCheckBoxColumn lEditVisibleCol = new DataGridViewCheckBoxColumn();
                lEditVisibleCol.Name = "EditVisible";
                lEditVisibleCol.DefaultCellStyle.NullValue = true;
                dgvFields.Columns.Add(lEditVisibleCol);

                DataGridViewCheckBoxColumn lCanSearchCol = new DataGridViewCheckBoxColumn();
                lCanSearchCol.Name = "CanSearch";
                lCanSearchCol.DefaultCellStyle.NullValue = true;
                dgvFields.Columns.Add(lCanSearchCol);
            }
        }

        private void btnGenerateEntityClass_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            ModelGenerator lModelGenerator = new ModelGenerator(lTable);
            lModelGenerator.GenerateModelClass();
        }

        private void GetSelectedInfo(ref TableInfo pTable)
        {
            if (lstTable.SelectedItem == null) return;

            pTable = new TableInfo();
            pTable.Columns = new List<ColumnInfo>();
            ColumnInfo lColumn = null;

            pTable.Name = lstTable.SelectedItem.ToString();
            for (int i = 0; i < dgvFields.Rows.Count; i++)
            {
                lColumn = new ColumnInfo();

                lColumn.Name = dgvFields.Rows[i].Cells["Name"].Value.ToString();
                lColumn.Type = dgvFields.Rows[i].Cells["Type"].Value.ToString();
                lColumn.Length = dgvFields.Rows[i].Cells["Length"].Value.ToString();
                lColumn.Comment = dgvFields.Rows[i].Cells["Comment"].Value.ToString();

                lColumn.ListVisible = Convert.ToBoolean(dgvFields.Rows[i].Cells["ListVisible"].Value ?? true);
                lColumn.NewVisible = Convert.ToBoolean(dgvFields.Rows[i].Cells["NewVisible"].Value ?? true);
                lColumn.EditVisible = Convert.ToBoolean(dgvFields.Rows[i].Cells["EditVisible"].Value ?? true);
                lColumn.CanSearch = Convert.ToBoolean(dgvFields.Rows[i].Cells["CanSearch"].Value ?? true);

                pTable.Columns.Add(lColumn);
            }
        }

        private void btnGenerateDao_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            DaoGenerator lDaoGenerator = new DaoGenerator(lTable);
            lDaoGenerator.GenerateDaoClass();
        }

        private void btnCreateHtmlListPage_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            ListHtmlGenerator lListHtmlGenerator = new ListHtmlGenerator(lTable);
            lListHtmlGenerator.GenerateListHtml();
        }

        private void btnCreateHandler_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            HandlerGenerator lHandlerGenerator = new HandlerGenerator(lTable);
            lHandlerGenerator.GenerateHandlerClass();
        }

        private void btnCreateHtmlNewPage_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            NewHtmlGenerator lNewHtmlGenerator = new NewHtmlGenerator(lTable);
            lNewHtmlGenerator.GenerateNewHtml();
        }

        private void btnCreatePagePS_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            PagingProcedureGenerator lPagingProcedureGenerator = new PagingProcedureGenerator(lTable);
            lPagingProcedureGenerator.GeneratePagingProcedure();
        }

        private void btnCreateHtmlEditPage_Click(object sender, EventArgs e)
        {
            TableInfo lTable = null;
            GetSelectedInfo(ref lTable);
            if (lTable == null) return;
            EditHtmlGenerator lEditHtmlGenerator = new EditHtmlGenerator(lTable);
            lEditHtmlGenerator.GenerateEditHtml();
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
