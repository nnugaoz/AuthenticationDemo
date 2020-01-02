using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DBHelper;
public class T_UserDao{
public static string InsertSQL(){string lSQL="";
lSQL += "INSERT INTO T_User(";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@Name";
lSQL += ",@EditMan";
lSQL += ",@EditDate";
lSQL += ")";
return lSQL;
}
public static string DeleteSQL(){
string lSQL="";
lSQL += "DELETE FROM T_User ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string UpdateSQL(){
string lSQL="";
lSQL += "UPDATE T_User SET ";
lSQL += "ID=@ID";
lSQL += ",Name=@Name";
lSQL += ",EditMan=@EditMan";
lSQL += ",EditDate=@EditDate";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_User";
return lSQL;
}
public static string SelectByIDSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_User";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectPageSQL(){
string lSQL="";
lSQL += "EXEC T_User_Page @Name,@BeginIndex,@EndIndex";
return lSQL;
}
public static string ImportSQL(){string lSQL="";
lSQL += "INSERT INTO T_User(";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@Name";
lSQL += ",@EditMan";
lSQL += ",@EditDate";
lSQL += ")";
return lSQL;
}
public int Insert(T_UserModel T_UserModel)
{
string lSQL = InsertSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_UserModel.ID));
lParams.Add(new SqlParameter("@Name", T_UserModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_UserModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_UserModel.EditDate));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
public int Update(T_UserModel T_UserModel)
{
string lSQL = UpdateSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_UserModel.ID));
lParams.Add(new SqlParameter("@Name", T_UserModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_UserModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_UserModel.EditDate));
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
public DataTable SelectPage(string Name, int BeginIndex,int EndIndex)
{
string lSQL = SelectPageSQL();
DataTable lDT = null;
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@Name", Name));
lParams.Add(new SqlParameter("@BeginIndex", BeginIndex));
lParams.Add(new SqlParameter("@EndIndex", EndIndex));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());
return lDT;
}
public DataTable SelectByID(string ID)
{
string lSQL = SelectByIDSQL();
DataTable lDT = null;
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", ID));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());
return lDT;
}
public int Import(string lFileName){
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
