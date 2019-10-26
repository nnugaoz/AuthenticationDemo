using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class LoginDao
    {
        public Boolean Check(string pUserName, string pPassword)
        {
            Boolean lRet = false;

            String lSQL = "SELECT 1";
            lSQL += " FROM T_User";
            lSQL += " WHERE UserName='" + pUserName + "' AND Pwd='" + pPassword + "'";

            MsSqlHelper lMsSql = new MsSqlHelper();

            DataSet lDS = lMsSql.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables[0].Rows.Count > 0)
            {
                lRet = true;
            }

            return lRet;
        }
    }
}