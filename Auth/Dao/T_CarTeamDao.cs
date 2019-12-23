using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Lib.DBHelper;

public class T_CarTeamDao{
public static string InsertSQL(){string lSQL="";
lSQL += "INSERT INTO T_CarTeam(";
lSQL += "ID";
lSQL += ",Name";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@Name";
lSQL += ")";
return lSQL;
}
public static string DeleteSQL(){
string lSQL="";
lSQL += "DELETE FROM T_CarTeam ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string UpdateSQL(){
string lSQL="";
lSQL += "UPDATE T_CarTeam SET ";
lSQL += "ID=@ID";
lSQL += ",Name=@Name";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += " FROM T_CarTeam";
return lSQL;
}
public static string SelectByIDSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += "FROM T_CarTeam";
lSQL += ")";
lSQL += "WHERE ID=@ID";
return lSQL;
}
public static string SelectPageSQL(){
string lSQL="";
lSQL += "EXEC T_CarTeam_Page @BeginIndex,@EndIndex";
return lSQL;
}
public int Insert(T_CarTeamModel T_CarTeamModel)
{
string lSQL = InsertSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_CarTeamModel.ID));
lParams.Add(new SqlParameter("@Name", T_CarTeamModel.Name));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
public int Update(T_CarTeamModel T_CarTeamModel)
{
string lSQL = UpdateSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_CarTeamModel.ID));
lParams.Add(new SqlParameter("@Name", T_CarTeamModel.Name));
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
public DataTable SelectPage(int BeginIndex,int EndIndex)
{
string lSQL = SelectPageSQL();
DataTable lDT = null;
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@BeginIndex", BeginIndex));
lParams.Add(new SqlParameter("@EndIndex", EndIndex));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());
return lDT;
}
}
