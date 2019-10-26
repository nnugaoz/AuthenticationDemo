using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class RoleDao
    {
        public DataTable GetByID(string pID)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            DataTable lDT = null;

            string lSQL = "";
            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += " FROM T_Role";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[] {
                new SqlParameter("@ID",pID)
            };

            DataSet lDS = null;

            lDS = lMsSqlHelper.GetData(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
            }


            return lDT;
        }

        public void Update(string lID, string lRName, string lFIDS)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL += "UPDATE T_Role SET";
            lSQL += " RName=@RName";
            lSQL += ", FIDS=@FIDS";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[] {
                new SqlParameter("@RName",lRName)
                ,new SqlParameter("@FIDS",lFIDS)
                ,new SqlParameter("@ID",lID)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lParams);

        }
    }
}