using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    public class FileHelper
    {
        public static void AppendLine(FileStream pFs, String pLine)
        {
            byte[] lData = Encoding.UTF8.GetBytes(pLine + "\r\n");
            pFs.Write(lData, 0, lData.Length);
        }
    }
}
