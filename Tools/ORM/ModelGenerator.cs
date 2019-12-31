using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class ModelGenerator
    {
        private TableInfo mTable = null;
        private string mModelPath = "Model/";

        public ModelGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GenerateModelClass()
        {
            String lTableName = ""; //表名
            String lEntityClassFileName = "";    //Model类名
            String lLine = "";

            if (mTable == null) return;

            lTableName = mTable.Name;
            lEntityClassFileName = lTableName + "Model.cs";

            if (!Directory.Exists(mModelPath))
            {
                Directory.CreateDirectory(mModelPath);
            }
            FileStream lFileStream = File.Create(mModelPath + @"\" + lEntityClassFileName);

            //类的声明
            lLine = "public class " + lTableName + "Model{";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "//" + mTable.Columns[i].Comment;
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "//MaxLength=" + mTable.Columns[i].Length;
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "public ";

                switch (mTable.Columns[i].Type)
                {
                    case "varchar":
                        lLine += "string ";
                        break;

                    case "decimal":
                        lLine += "decimal ";
                        break;

                    default:
                        lLine += "string ";
                        break;
                }

                //字段声明
                lLine += mTable.Columns[i].Name + "{get;set;}";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            //类声明结束
            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }
    }
}
