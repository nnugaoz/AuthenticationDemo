using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace Lib
{
    public class NPOIExcelHelper
    {
        private DataTable ImportToDataTable(ISheet pSheet)
        {
            DataTable lDT = new DataTable();
            for (int i = pSheet.FirstRowNum; i < pSheet.LastRowNum; i++)
            {
                if (i == pSheet.FirstRowNum)
                {
                    for (int j = pSheet.GetRow(i).FirstCellNum; j < pSheet.GetRow(i).LastCellNum; j++)
                    {
                        DataColumn lCol = new DataColumn();
                        lCol.DataType = "string".GetType();
                        lCol.ColumnName = pSheet.GetRow(i).GetCell(j).StringCellValue;
                        lDT.Columns.Add(lCol);
                    }
                }
                else
                {
                    int k = 0;
                    DataRow lRow = lDT.NewRow();
                    for (int j = pSheet.GetRow(i).FirstCellNum; j < pSheet.GetRow(i).LastCellNum; j++)
                    {
                        lRow[k++] = pSheet.GetRow(i).GetCell(j).StringCellValue;
                    }
                    lDT.Rows.Add(lRow);
                }
            }
            return lDT;
        }

        public DataSet ImportToDataSet(string pXLSXFile)
        {
            DataSet lDS = null;
            DataTable lDT = null;
            FileStream lFS = new FileStream(pXLSXFile, FileMode.Open, FileAccess.Read);

            XSSFWorkbook lWK = new XSSFWorkbook(lFS);

            if (lWK.NumberOfSheets > 0)
            {
                lDS = new DataSet();
                for (int i = 0; i < lWK.NumberOfSheets; i++)
                {
                    lDT = ImportToDataTable(lWK.GetSheetAt(i));
                    lDS.Tables.Add(lDT);
                }
            }
            return lDS;
        }

        private bool DataTableExport(DataTable pDT, ISheet pSheet)
        {
            bool lRet = false;
            int lStartRowIdx = 0;
            int lStartColIdx = 0;

            for (int i = 0; i < pDT.Columns.Count; i++)
            {
                if (i == 0)
                {
                    pSheet.CreateRow(lStartRowIdx);
                }
                pSheet.GetRow(lStartRowIdx).CreateCell(lStartColIdx + i).SetCellValue(pDT.Columns[i].ColumnName);
            }
            lStartRowIdx++;

            for (int i = 0; i < pDT.Rows.Count; i++)
            {
                pSheet.CreateRow(lStartRowIdx + i);

                for (int j = 0; j < pDT.Columns.Count; j++)
                {
                    pSheet.GetRow(lStartRowIdx + i).CreateCell(lStartColIdx + j).SetCellValue(pDT.Rows[i][j].ToString());
                }
            }

            return lRet;
        }

        public bool DataSetExport(DataSet pDS, ref string pFileName)
        {
            bool lRet = false;
            if (pDS != null && pDS.Tables.Count > 0)
            {
                XSSFWorkbook lWB = new XSSFWorkbook();
                for (int i = 0; i < pDS.Tables.Count; i++)
                {
                    ISheet lSheet = lWB.CreateSheet(pDS.Tables[i].TableName);
                    DataTableExport(pDS.Tables[i], lSheet);
                }

                pFileName = Guid.NewGuid().ToString() + ".xlsx";
                FileStream lFS = File.Create(@"E:\02_Git\01_Auth\Auth\TempFile\" + pFileName);
                lWB.Write(lFS);
            }
            return lRet;
        }

    }
}
