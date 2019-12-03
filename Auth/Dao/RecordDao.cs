using System.Data;

namespace Auth.Dao
{
    class RecordDao
    {
        internal DataTable GetListPagination(int pBeginIndex, int pEndIndex)
        {
            DataTable lDT = null;
            lDT = CommonDao.MsSqlQueryDataPagination("T_Weight", "ID", pBeginIndex, pEndIndex);
            return lDT;
        }


    }
}
