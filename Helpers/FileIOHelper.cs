using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataCheckTool.Helpers
{
    public class FileIOHelper
    {
        private static List<string> _pathList = new List<string>();

        /// <summary>
        /// Create target file
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="fileName">folder name</param>
        /// <param name="fileExtension">file extension</param>
        /// <returns>file path</returns>
        public static string CreateFile(string folder, string fileName, string fileExtension)
        {
            string filePath = $"{folder}\\{fileName}.{fileExtension}";

            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filePath;
        }

        /// <summary>
        /// 通过路径递归获取该路径下的所有子目录
        /// </summary>
        /// <param name="rootPath">路径</param>
        /// <returns>路径下的全部子目录合集</returns>
        public static List<string> GetAllSubdirectoryByPath(string rootPath)
        {
            var data = Directory.GetDirectories(rootPath);

            foreach (var item in data)
            {
                _pathList.Add(item);
            }

            foreach (var item in data)
            {
                GetAllSubdirectoryByPath(item);
            }

            return _pathList;
        }

        /// <summary>
        /// 获取指定路径下的全部文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="searchPattern">搜索模式</param>
        /// <returns></returns>
        public static List<string> GetAllFilesByPath(string path, string searchPattern = "*")
        {
            var result = new List<string>();
            var data = Directory.GetFiles(path, searchPattern);

            if (data.Length > 0)
            {
                result = data.ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取路径节点数最大值
        /// </summary>
        /// <param name="pathList"></param>
        /// <returns></returns>
        public static int GetPathNodeMaxNums(List<string> pathList)
        {
            var maxNum = 0;
            var numList = new List<int>();

            foreach (var path in pathList)
            {
                var length = path.Split(@"\").Length;
                numList.Add(length);
            }

            maxNum = numList.Max();

            return maxNum;
        }
    }
}
