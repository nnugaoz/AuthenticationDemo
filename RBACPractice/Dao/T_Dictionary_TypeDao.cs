using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DBHelper;

public class T_Dictionary_TypeDao{
public static string InsertSQL(){string lSQL="";
lSQL += "INSERT INTO T_Dictionary_Type(";
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
lSQL += "DELETE FROM T_Dictionary_Type ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string UpdateSQL(){
string lSQL="";
lSQL += "UPDATE T_Dictionary_Type SET ";
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
lSQL += " FROM T_Dictionary_Type";
return lSQL;
}
public static string SelectByIDSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_Dictionary_Type";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectPageSQL(){
string lSQL="";
lSQL += "EXEC T_Dictionary_Type_Page @Name,@BeginIndex,@EndIndex";
return lSQL;
}
public static string ImportSQL(){string lSQL="";
lSQL += "INSERT INTO T_Dictionary_Type(";
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
public int Insert(T_Dictionary_TypeModel T_Dictionary_TypeModel)
{
string lSQL = InsertSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_Dictionary_TypeModel.ID));
lParams.Add(new SqlParameter("@Name", T_Dictionary_TypeModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_Dictionary_TypeModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_Dictionary_TypeModel.EditDate));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
public int Update(T_Dictionary_TypeModel T_Dictionary_TypeModel)
{
string lSQL = UpdateSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_Dictionary_TypeModel.ID));
lParams.Add(new SqlParameter("@Name", T_Dictionary_TypeModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_Dictionary_TypeModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_Dictionary_TypeModel.EditDate));
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
