using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class PagingProcedureGenerator
    {
        private TableInfo mTable = null;
        private String mPagingProcedurePath = "Procedure/Page/";

        public PagingProcedureGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GeneratePagingProcedure()
        {

            if (mTable == null) return;


            String lTableName = mTable.Name;

            if (!Directory.Exists(mPagingProcedurePath + @"/"))
            {
                Directory.CreateDirectory(mPagingProcedurePath + @"/");
            }

            FileStream lFileStream = new FileStream(mPagingProcedurePath + @"/" + lTableName + "_Page.sql", FileMode.Create, FileAccess.ReadWrite);
            String lLine = "";

            lLine = "IF OBJECT_ID('" + lTableName + "_Page') IS NOT NULL";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "BEGIN ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DROP PROCEDURE " + lTableName + "_Page ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "END ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "GO ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "CREATE PROCEDURE " + lTableName + "_Page ";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;
                lLine = "@" + mTable.Columns[i].Name + " VARCHAR(50) ,";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "@BeginIndex INT";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", @EndIndex INT";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "AS";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "BEGIN";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "SET NOCOUNT ON;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DECLARE @RowCnt AS INT";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "SELECT @RowCnt = COUNT(*) FROM " + lTableName + " WHERE 1=1 ";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;
                lLine = "AND " + mTable.Columns[i].Name + " LIKE '%'+@" + mTable.Columns[i].Name + "+'%'";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "SELECT * FROM(";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "SELECT";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "ROW_NUMBER()OVER(ORDER BY ID) RowIdx";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", @RowCnt RowCnt";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " , *";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "FROM " + lTableName + " WHERE 1=1 ";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;
                lLine = "AND " + mTable.Columns[i].Name + " LIKE '%'+@" + mTable.Columns[i].Name + "+'%'";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = ")T";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "WHERE RowIdx >= @BeginIndex AND RowIdx <= @EndIndex";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "END";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "GO ";
            FileHelper.AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }
    }
}
