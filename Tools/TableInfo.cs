using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    class TableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        public List<ColumnInfo> Columns { get; set; }
    }
}
