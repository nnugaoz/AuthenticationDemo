using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Lib.DBHelper;
public class T_ProductDao{
public static string InsertSQL(){string lSQL="";
lSQL += "INSERT INTO T_Product(";
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
lSQL += "DELETE FROM T_Product ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string UpdateSQL(){
string lSQL="";
lSQL += "UPDATE T_Product SET ";
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
lSQL += " FROM T_Product";
return lSQL;
}
public static string SelectByIDSQL(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",Name";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += " FROM T_Product";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public static string SelectPageSQL(){
string lSQL="";
lSQL += "EXEC T_Product_Page @BeginIndex,@EndIndex";
return lSQL;
}
public int Insert(T_ProductModel T_ProductModel)
{
string lSQL = InsertSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_ProductModel.ID));
lParams.Add(new SqlParameter("@Name", T_ProductModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_ProductModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_ProductModel.EditDate));
MsSqlHelper lMSSqlHelper = new MsSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
public int Update(T_ProductModel T_ProductModel)
{
string lSQL = UpdateSQL();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", T_ProductModel.ID));
lParams.Add(new SqlParameter("@Name", T_ProductModel.Name));
lParams.Add(new SqlParameter("@EditMan", T_ProductModel.EditMan));
lParams.Add(new SqlParameter("@EditDate", T_ProductModel.EditDate));
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
}
