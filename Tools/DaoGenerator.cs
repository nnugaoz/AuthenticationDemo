using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class DaoGenerator
    {
        private TableInfo mTable = null;
        private String mDaoPath = "Dao/";

        public DaoGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GenerateDaoClass()
        {
            String lTableName = "";
            String lDaoClassFileName = "";
            String lLine = "";

            if (mTable == null) return;

            lTableName = mTable.Name;
            lDaoClassFileName = lTableName + "Dao.cs";

            if (!Directory.Exists(mDaoPath))
            {
                Directory.CreateDirectory(mDaoPath);
            }

            FileStream lFileStream = File.Create(mDaoPath + @"\" + lDaoClassFileName);

            lLine = "using System.Collections.Generic;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "using System.Data.SqlClient;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "using System.Data;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "public class " + lTableName + "Dao{";
            FileHelper.AppendLine(lFileStream, lLine);

            Generate_InsertSQL_Function(lFileStream);

            Generate_DeleteSQL_Function(lFileStream);

            Generate_UpdateSQL_Function(lFileStream);

            Generate_SelectSQL_Function(lFileStream);

            Generate_SelectByIDSQL_Function(lFileStream);

            Generate_SelectPageSQL_Function(lFileStream);

            Generate_Insert_Function(lFileStream);

            Generate_Update_Function(lFileStream);

            Generate_Delete_Function(lFileStream);

            Generate_Select_Function(lFileStream);

            Generate_SelectPage_Function(lFileStream);

            Generate_SelectByID_Function(lFileStream);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void Generate_SelectByID_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public DataTable SelectByID(string ID)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectByIDSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@ID\", ID));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());";

            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lDT;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectPage_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public DataTable SelectPage(";

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].CanSearch) continue;
                lLine += "string " + mTable.Columns[i].Name + ", ";

            }
            lLine += "int BeginIndex,int EndIndex)";

            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectPageSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].CanSearch) continue;
                lLine = "lParams.Add(new SqlParameter(\"@" + mTable.Columns[i].Name + "\", " + mTable.Columns[i].Name + "));";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "lParams.Add(new SqlParameter(\"@BeginIndex\", BeginIndex));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@EndIndex\", EndIndex));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL,lParams.ToArray());";

            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lDT;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

        }

        private void Generate_Select_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public DataTable Select()";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = SelectSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DataTable lDT = null;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lDT = lMSSqlHelper.GetDataTable(lSQL);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lDT;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

        }

        private void Generate_Delete_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public int Delete(string pID)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = DeleteSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lParams.Add(new SqlParameter(\"@ID\", pID));";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Update_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public int Update(" + mTable.Name + "Model " + mTable.Name + "Model)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = UpdateSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "lParams.Add(new SqlParameter(\"@" + mTable.Columns[i].Name + "\", " + mTable.Name + "Model." + mTable.Columns[i].Name + "));";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Insert_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public int Insert(" + mTable.Name + "Model " + mTable.Name + "Model)";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "string lSQL = InsertSQL();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "List<SqlParameter> lParams = new List<SqlParameter>();";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "lParams.Add(new SqlParameter(\"@" + mTable.Columns[i].Name + "\", " + mTable.Name + "Model." + mTable.Columns[i].Name + "));";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "MsSqlHelper lMSSqlHelper = new MsSqlHelper();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectPageSQL_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public static string SelectPageSQL(){\r\n";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"EXEC " + mTable.Name + "_Page ";
            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].CanSearch) continue;
                lLine += "@" + mTable.Columns[i].Name + ",";
            }
            lLine += "@BeginIndex,@EndIndex\";\r\n";
            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectByIDSQL_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public static string SelectByIDSQL(){\r\n";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"SELECT \";\r\n";
            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + mTable.Columns[i].Name + "\";\r\n";
                }
                else
                {
                    lLine += "lSQL += \"," + mTable.Columns[i].Name + "\";\r\n";
                }
            }
            lLine += "lSQL += \" FROM " + mTable.Name + "\";\r\n";
            lLine += "lSQL += \" WHERE ID=@ID\";\r\n";
            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_SelectSQL_Function(FileStream lFileStream)
        {
            String lLine = "";
            //SELECT
            lLine = "public static string SelectSQL(){\r\n";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"SELECT \";\r\n";
            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + mTable.Columns[i].Name + "\";\r\n";
                }
                else
                {
                    lLine += "lSQL += \"," + mTable.Columns[i].Name + "\";\r\n";
                }
            }
            lLine += "lSQL += \" FROM " + mTable.Name + "\";\r\n";

            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_UpdateSQL_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public static string UpdateSQL(){\r\n";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"UPDATE " + mTable.Name + " SET \";\r\n";
            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + mTable.Columns[i].Name + "=@" + mTable.Columns[i].Name + "\";\r\n";
                }
                else
                {
                    lLine += "lSQL += \"," + mTable.Columns[i].Name + "=@" + mTable.Columns[i].Name + "\";\r\n";
                }
            }
            lLine += "lSQL += \" WHERE ID=@ID\";\r\n";
            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_DeleteSQL_Function(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "public static string DeleteSQL(){\r\n";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"DELETE FROM " + mTable.Name + " \";\r\n";
            lLine += "lSQL += \" WHERE ID=@ID\";\r\n";
            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_InsertSQL_Function(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "public static string InsertSQL(){";
            lLine += "string lSQL=\"\";\r\n";
            lLine += "lSQL += \"INSERT INTO " + mTable.Name + "(\";\r\n";

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"" + mTable.Columns[i].Name + "\";\r\n";
                }
                else
                {
                    lLine += "lSQL += \"," + mTable.Columns[i].Name + "\";\r\n";
                }
            }

            lLine += "lSQL += \")VALUES(\";\r\n";

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (i == 0)
                {
                    lLine += "lSQL += \"@" + mTable.Columns[i].Name + "\";\r\n";
                }
                else
                {
                    lLine += "lSQL += \",@" + mTable.Columns[i].Name + "\";\r\n";
                }
            }

            lLine += "lSQL += \")\";\r\n";
            lLine += "return lSQL;\r\n";
            lLine += "}";
            FileHelper.AppendLine(lFileStream, lLine);
        }
    }
}
