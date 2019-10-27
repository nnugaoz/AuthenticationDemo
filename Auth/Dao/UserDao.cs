using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class UserDao
    {

        public void AddUser(string pUserName, string pPwd, string pRIDS)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "INSERT INTO T_User(";
            lSQL += "ID";
            lSQL += ", UserName";
            lSQL += ", Pwd";
            lSQL += ", RIDS";
            lSQL += ")VALUES(";
            lSQL += "@ID";
            lSQL += ", @UserName";
            lSQL += ", @Pwd";
            lSQL += ", @RIDS";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[]{
                new SqlParameter("@UserName",pUserName)
                ,new SqlParameter("@Pwd",pPwd)
                ,new SqlParameter("@RIDS",pRIDS)
                ,new  SqlParameter("@ID",Guid.NewGuid().ToString())
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        public void EditUser(string pID, string pUserName, string pRIDS)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "UPDATE T_User SET";
            lSQL += " UserName=@UserName";
            lSQL += ", RIDS=@RIDS";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lSqlParams = new SqlParameter[]{
                new SqlParameter("@UserName",pUserName)
                ,new SqlParameter("@RIDS",pRIDS)
                ,new  SqlParameter("@ID",pID)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        public DataTable GetUserById(string lID)
        {
            
        }

        public DataTable GetUserList()
        {
            DataTable lDTUser = null;
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            string lSQL = "SELECT ";
            lSQL += " ID";
            lSQL += ", UserName";
            lSQL += ", RIDS";
            lSQL += " FROM T_User";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTUser = lDS.Tables[0];
                lDTUser.TableName = "UserList";
            }

            return lDTUser;
        }
    }
}