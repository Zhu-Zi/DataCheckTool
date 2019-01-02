using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataCheckTool.Helpers
{
    public class CSVHelper
    {
        public static void SaveDataToCSV(string folderPath, string fileName, List<string> infoStr)
        {
            var filePath = FileIOHelper.CreateFile(folderPath, fileName, "csv");

            StringBuilder strColumn = new StringBuilder();
            StringBuilder strValue = new StringBuilder();
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(filePath);

                #region 设置列

                strColumn.Append("Info");

                #endregion

                strColumn.Remove(strColumn.Length - 1, 1);
                sw.WriteLine(strColumn);    //// 打印列名

                foreach (var item in infoStr)
                {
                    strValue.Remove(0, strValue.Length); //// 清除临时行值 clear the temp row value
                    strValue.Append(item);
                    sw.WriteLine(strValue); //// 写入行数据
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }
        }
    }
}
