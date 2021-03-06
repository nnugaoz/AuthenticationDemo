using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DBHelper;
public class T_PermissionDao
{
    public static string InsertSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_Permission(";
        lSQL += "ID";
        lSQL += ",Name";
        lSQL += ",PID";
        lSQL += ",Sort";
        lSQL += ",Addr";
        lSQL += ",TypeDID";
        lSQL += ",RIdentify";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@Name";
        lSQL += ",@PID";
        lSQL += ",@Sort";
        lSQL += ",@Addr";
        lSQL += ",@TypeDID";
        lSQL += ",@RIdentify";
        lSQL += ",@EditMan";
        lSQL += ",@EditDate";
        lSQL += ")";
        return lSQL;
    }
    public static string DeleteSQL()
    {
        string lSQL = "";
        lSQL += "DELETE FROM T_Permission ";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string UpdateSQL()
    {
        string lSQL = "";
        lSQL += "UPDATE T_Permission SET ";
        lSQL += "ID=@ID";
        lSQL += ",Name=@Name";
        lSQL += ",PID=@PID";
        lSQL += ",Sort=@Sort";
        lSQL += ",Addr=@Addr";
        lSQL += ",EditMan=@EditMan";
        lSQL += ",EditDate=@EditDate";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string SelectSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",Name";
        lSQL += ",PID";
        lSQL += ",Sort";
        lSQL += ",Addr";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += " FROM T_Permission";
        lSQL += " ORDER BY Sort";
        return lSQL;
    }

    public static string SelectByIDSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",Name";
        lSQL += ",PID";
        lSQL += ",Sort";
        lSQL += ",Addr";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += " FROM T_Permission";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string SelectPageSQL()
    {
        string lSQL = "";
        lSQL += "EXEC T_Permission_Page @Name,@PID,@Sort,@Addr,@BeginIndex,@EndIndex";
        return lSQL;
    }
    public static string ImportSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_Permission(";
        lSQL += "ID";
        lSQL += ",Name";
        lSQL += ",PID";
        lSQL += ",Sort";
        lSQL += ",Addr";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@Name";
        lSQL += ",@PID";
        lSQL += ",@Sort";
        lSQL += ",@Addr";
        lSQL += ",@EditMan";
        lSQL += ",@EditDate";
        lSQL += ")";
        return lSQL;
    }

    private string SelectMenuByUserNameSQL()
    {
        string lSQL = "";
        lSQL += "SELECT T_Permission.ID";
        lSQL += " ,T_Permission.Name";
        lSQL += " ,T_Permission.PID";
        lSQL += " ,T_Permission.Sort";
        lSQL += " ,T_Permission.Addr";
        lSQL += " ,T_Permission.RIdentify";
        lSQL += " ,T_Permission.EditMan";
        lSQL += " ,T_Permission.EditDate";
        lSQL += " FROM T_User";
        lSQL += " LEFT JOIN T_User_Role ON T_User.ID = T_User_Role.UID";
        lSQL += " LEFT JOIN T_Role_Permission ON T_User_Role.RID = T_Role_Permission.RID";
        lSQL += " LEFT JOIN T_Permission ON T_Role_Permission.MID = T_Permission.ID";
        lSQL += " WHERE T_User.Name = @UserName";
        return lSQL;
    }

    private string SelectSortNumberAvailableSQL()
    {
        string lSQL = "";
        lSQL += "SELECT dbo.SF_GetPermissionSortNumberAvailable(@PID)";
        return lSQL;
    }

    public int Insert(T_PermissionModel T_PermissionModel)
    {
        string lSQL = "";
        List<SqlParameter> lParams = null;
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        DataTable lDT = null;
        int lSortNumberAvailable = 0;

        lSQL = SelectSortNumberAvailableSQL();
        lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@PID", T_PermissionModel.PID));
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        if (lDT != null && lDT.Rows.Count > 0)
        {
            lSortNumberAvailable = Convert.ToInt32(lDT.Rows[0][0].ToString());
        }

        if (lSortNumberAvailable != 0)
        {
            T_PermissionModel.Sort = lSortNumberAvailable.ToString();

            lSQL = InsertSQL();
            lParams = new List<SqlParameter>();
            lParams.Add(new SqlParameter("@ID", T_PermissionModel.ID));
            lParams.Add(new SqlParameter("@Name", T_PermissionModel.Name));
            lParams.Add(new SqlParameter("@PID", T_PermissionModel.PID));
            lParams.Add(new SqlParameter("@Sort", T_PermissionModel.Sort));
            lParams.Add(new SqlParameter("@Addr", T_PermissionModel.Addr));
            lParams.Add(new SqlParameter("@TypeDID", T_PermissionModel.TypeDID));
            lParams.Add(new SqlParameter("@RIdentify", T_PermissionModel.RIdentify));
            lParams.Add(new SqlParameter("@EditMan", T_PermissionModel.EditMan));
            lParams.Add(new SqlParameter("@EditDate", T_PermissionModel.EditDate));
            return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
        }
        else
        {
            return -1;
        }
    }

    public int Update(T_PermissionModel T_PermissionModel)
    {
        string lSQL = UpdateSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_PermissionModel.ID));
        lParams.Add(new SqlParameter("@Name", T_PermissionModel.Name));
        lParams.Add(new SqlParameter("@PID", T_PermissionModel.PID));
        lParams.Add(new SqlParameter("@Sort", T_PermissionModel.Sort));
        lParams.Add(new SqlParameter("@Addr", T_PermissionModel.Addr));
        lParams.Add(new SqlParameter("@EditMan", T_PermissionModel.EditMan));
        lParams.Add(new SqlParameter("@EditDate", T_PermissionModel.EditDate));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }

    public int Delete(string pID)
    {
        string lSQL = DeleteSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", pID));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }

    public DataTable Select()
    {
        string lSQL = SelectSQL();
        DataTable lDT = null;
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL);
        return lDT;
    }

    public DataTable SelectPage(string Name, string PID, string Sort, string Addr, int BeginIndex, int EndIndex)
    {
        string lSQL = SelectPageSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@Name", Name));
        lParams.Add(new SqlParameter("@PID", PID));
        lParams.Add(new SqlParameter("@Sort", Sort));
        lParams.Add(new SqlParameter("@Addr", Addr));
        lParams.Add(new SqlParameter("@BeginIndex", BeginIndex));
        lParams.Add(new SqlParameter("@EndIndex", EndIndex));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }

    public DataTable SelectByID(string ID)
    {
        string lSQL = SelectByIDSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", ID));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }

    internal DataTable SelectMenuByUserName(string UserName)
    {
        string lSQL = SelectMenuByUserNameSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@UserName", UserName));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }

    public int Import(string lFileName)
    {
        int lRet = -1;
        DataSet lDS = null;
        DataTable lDT = null;
        NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
        Dictionary<string, SqlParameter[]> lSqlDic = new Dictionary<string, SqlParameter[]>();
        string lSQL = "";
        MsSqlHelper lMsSqlHelper = new MsSqlHelper();
        lDS = lExcelHelper.ImportToDataSet(lFileName);
        if (lDS != null && lDS.Tables.Count > 0)
        {
            lDT = lDS.Tables[0];
            for (var i = 0; i < lDT.Rows.Count; i++)
            {
                lSQL = ImportSQL();
                SqlParameter[] lParams = new SqlParameter[]
                {
                    new SqlParameter("@ID",Guid.NewGuid().ToString())
                      ,new SqlParameter("@Name",lDT.Rows[i]["Name"].ToString())
                      ,new SqlParameter("@PID",lDT.Rows[i]["PID"].ToString())
                      ,new SqlParameter("@Sort",lDT.Rows[i]["Sort"].ToString())
                      ,new SqlParameter("@Addr",lDT.Rows[i]["Addr"].ToString())
                     ,new SqlParameter("@EditMan","Admin")
                      ,new SqlParameter("@EditDate",DateTime.Now.ToString("yyyy - MM - dd HH: mm: ss"))
                };
                lSqlDic.Add(lSQL + new string(' ', i), lParams);
            }
            lRet = lMsSqlHelper.ExecuteSQLWithTransaction(lSqlDic);
        }
        return lRet;
    }

    internal void Export(ref string lExcelFilePath)
    {
        NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
        DataSet lDS = new DataSet();
        DataTable lDT = new DataTable();
        lDT = Select();
        lDS.Tables.Add(lDT.Copy());
        lExcelHelper.DataSetExport(lDS, ref lExcelFilePath);
    }
}
