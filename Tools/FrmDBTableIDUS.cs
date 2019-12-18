using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                lSQL += " SELECT syscol.name FROM ";
                lSQL += " sys.tables systab left join sys.columns syscol on systab.object_id=syscol.object_id";
                lSQL += " WHERE systab.name = '" + lTableName + "'";
                lSQL += " ORDER BY syscol.column_id";

                SqlCommand lSqlCommand = new SqlCommand(lSQL, lConnection);
                SqlDataAdapter lSqlDataAdapter = new SqlDataAdapter(lSqlCommand);
                lSqlDataAdapter.Fill(lDT);
            }

            lstField.Items.Clear();
            for (int i = 0; i < lDT.Rows.Count; i++)
            {
                lstField.Items.Add(lDT.Rows[i][0].ToString());
            }
        }

        private void btnGenerateEntityClass_Click(object sender, EventArgs e)
        {

        }
    }
}
