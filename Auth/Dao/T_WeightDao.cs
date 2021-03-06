using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Lib.DBHelper;

public class T_WeightDao{
public static string InsertSQL(){string lSQL="";
lSQL += "INSERT INTO T_Weight(";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",ProductName";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@CarNO";
lSQL += ",@ProductName";
lSQL += ",@Gross";
lSQL += ",@Tare";
lSQL += ",@Net";
lSQL += ",@GrossTime";
lSQL += ",@TareTime";
lSQL += ",@NetTime";
lSQL += ",@EditMan";
lSQL += ",@EditDate";
lSQL += ")";
return lSQL;
}
public static string DeleteSQL(){
string lSQL="";
lSQL += "DELETE FROM T_Weight ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string UpdateSQL(){
string lSQL="";
lSQL += "UPDATE T_Weight SET ";
lSQL += "ID=@ID";
lSQL += ",CarNO=@CarNO";
lSQL += ",ProductName=@ProductName";
lSQL += ",Gross=@Gross";
lSQL += ",Tare=@Tare";
lSQL += ",Net=@Net";
lSQL += ",GrossTime=@GrossTime";
lSQL += ",TareTime=@TareTime";
lSQL += ",NetTime=@NetTime";
lSQL += ",EditMan=@EditMan";
lSQL += ",EditDate=@EditDate";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",ProductName";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_Weight";
return lSQL;
}
public static string SelectByIDSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",ProductName";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_Weight";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectPageSQL(){
string lSQL="";
lSQL += "EXEC T_Weight_Page @CarNO,@ProductName,@Gross,@Tare,@Net,@GrossTime,@TareTime,@NetTime,@EditMan,@EditDate,@BeginIndex,@EndIndex";
return lSQL;
}
public static string ImportSQL(){string lSQL="";
lSQL += "INSERT INTO T_Weight(";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",ProductName";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@CarNO";
lSQL += ",@ProductName";
lSQL += ",@Gross";
lSQL += ",@Tare";
lSQL += ",@Net";
lSQL += ",@GrossTime";
lSQL += ",@TareTime";
lSQL += ",@NetTime";
lSQL += ",@EditMan";
lSQL += ",@EditDate";
lSQL += ")";
return lSQL;
}
public int Insert(T_WeightModel T_WeightModel)
{
string lSQL = InsertSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_WeightModel.ID));
lParams.Add(new SqlParameter("@CarNO", T_WeightModel.CarNO));
lParams.Add(new SqlParameter("@ProductName", T_WeightModel.ProductName));
lParams.Add(new SqlParameter("@Gross", T_WeightModel.Gross));
lParams.Add(new SqlParameter("@Tare", T_WeightModel.Tare));
lParams.Add(new SqlParameter("@Net", T_WeightModel.Net));
lParams.Add(new SqlParameter("@GrossTime", T_WeightModel.GrossTime));
lParams.Add(new SqlParameter("@TareTime", T_WeightModel.TareTime));
lParams.Add(new SqlParameter("@NetTime", T_WeightModel.NetTime));
lParams.Add(new SqlParameter("@EditMan", T_WeightModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_WeightModel.EditDate));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
public int Update(T_WeightModel T_WeightModel)
{
string lSQL = UpdateSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_WeightModel.ID));
lParams.Add(new SqlParameter("@CarNO", T_WeightModel.CarNO));
lParams.Add(new SqlParameter("@ProductName", T_WeightModel.ProductName));
lParams.Add(new SqlParameter("@Gross", T_WeightModel.Gross));
lParams.Add(new SqlParameter("@Tare", T_WeightModel.Tare));
lParams.Add(new SqlParameter("@Net", T_WeightModel.Net));
lParams.Add(new SqlParameter("@GrossTime", T_WeightModel.GrossTime));
lParams.Add(new SqlParameter("@TareTime", T_WeightModel.TareTime));
lParams.Add(new SqlParameter("@NetTime", T_WeightModel.NetTime));
lParams.Add(new SqlParameter("@EditMan", T_WeightModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_WeightModel.EditDate));
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
public DataTable SelectPage(string CarNO, string ProductName, string Gross, string Tare, string Net, string GrossTime, string TareTime, string NetTime, string EditMan, string EditDate, int BeginIndex,int EndIndex)
{
string lSQL = SelectPageSQL();
DataTable lDT = null;
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@CarNO", CarNO));
lParams.Add(new SqlParameter("@ProductName", ProductName));
lParams.Add(new SqlParameter("@Gross", Gross));
lParams.Add(new SqlParameter("@Tare", Tare));
lParams.Add(new SqlParameter("@Net", Net));
lParams.Add(new SqlParameter("@GrossTime", GrossTime));
lParams.Add(new SqlParameter("@TareTime", TareTime));
lParams.Add(new SqlParameter("@NetTime", NetTime));
lParams.Add(new SqlParameter("@EditMan", EditMan));
lParams.Add(new SqlParameter("@EditDate", EditDate));
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
      ,new SqlParameter("@CarNO",lDT.Rows[i]["CarNO"].ToString())
      ,new SqlParameter("@ProductName",lDT.Rows[i]["ProductName"].ToString())
      ,new SqlParameter("@Gross",lDT.Rows[i]["Gross"].ToString())
      ,new SqlParameter("@Tare",lDT.Rows[i]["Tare"].ToString())
      ,new SqlParameter("@Net",lDT.Rows[i]["Net"].ToString())
      ,new SqlParameter("@GrossTime",lDT.Rows[i]["GrossTime"].ToString())
      ,new SqlParameter("@TareTime",lDT.Rows[i]["TareTime"].ToString())
      ,new SqlParameter("@NetTime",lDT.Rows[i]["NetTime"].ToString())
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
