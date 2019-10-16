using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth
{
    public class MsSqlAccess
    {
        public DataTable GetData(string pSQL)
        {
            DataTable lDT = null;
            using (SqlConnection lSqlConnection = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=Auth;Persist Security Info=True;User ID=sa;Password=1;"))
            {
                SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCommand);

                DataSet lDS = null;

                lSqlAdpt.Fill(lDS);

                if (lDS != null && lDS.Tables.Count > 0)
                {
                    lDT = lDS.Tables[0];
                }
            }
            return lDT;
        }


    }
}