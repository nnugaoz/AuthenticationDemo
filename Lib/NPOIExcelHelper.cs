using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;

namespace Lib
{
    public class NPOIExcelHelper
    {
        public DataTable LoadToDataTable(ISheet pSheet)
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

        public DataSet LoadToDataSet(string pXLSXFile)
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
                    lDT = LoadToDataTable(lWK.GetSheetAt(i));
                    lDS.Tables.Add(lDT);
                }
            }
            return lDS;
        }
    }
}
