using Auth.DBHelper;
using Auth.Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
