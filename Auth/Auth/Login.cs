using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Auth
{
    public class Login
    {
        public Boolean Check(string pUserName, string pPassword)
        {
            Boolean lRet = true;

            String lSQL = "SELECT 1";
            lSQL += " FROM T_User";
            lSQL += " WHERE UserName='" + pUserName + "' AND Password='" + pPassword + "'";

            MsSqlAccess lMsSql = new MsSqlAccess();

            DataTable lDT = lMsSql.GetData(lSQL);

            if (lDT != null && lDT.Rows.Count > 0)
            {
                lRet = true;
            }

            return lRet;
        }
    }
}