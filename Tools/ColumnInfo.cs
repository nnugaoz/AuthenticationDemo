using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    class ColumnInfo
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// 列名备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 列表页面是否可见
        /// </summary>
        public bool ListVisible { get; set; }

        /// <summary>
        /// 新增页面是否可见
        /// </summary>
        public bool NewVisible { get; set; }

        /// <summary>
        /// 编辑页面是否可见
        /// </summary>
        public bool EditVisible { get; set; }

        /// <summary>
        /// 检索字段标志位
        /// </summary>
        public bool SearchFlg { get; set; }

        /// <summary>
        /// 导入字段标志位
        /// </summary>
        public bool ImportFlg { get; set; }

        /// <summary>
        /// 导出字段标志位
        /// </summary>
        public bool ExportFlg { get; set; }

    }
}
