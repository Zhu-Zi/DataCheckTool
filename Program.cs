using System;
using System.Collections.Generic;
using System.Linq;
using DataCheckTool.Helpers;

namespace DataCheckTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pealse Input Root Path");
            var input = Console.ReadLine();
            var path = input;
            var allFilesPathList = FileIOHelper.GetAllSubdirectoryByPath(path);
            var pathMaxNodes = FileIOHelper.GetPathNodeMaxNums(allFilesPathList);
            var filesPathList = new List<string>();
            var allFileList = new List<string>();
            var missingDataList = new List<string>();

            foreach (var filePath in allFilesPathList)
            {
                var nodes = filePath.Split(@"\").Length;

                if (nodes == pathMaxNodes)
                {
                    ////filesPathList.Add(filePath);
                    var fileList = FileIOHelper.GetAllFilesByPath(filePath, "*.csv");

                    #region 检查数据
                    var isExist_btc = false;
                    var isExist_eth = false;
                    foreach (var file in fileList)
                    {
                        var item = file.Split("_").ToList();
                        var lastItem = item.Last().Split(".").First();

                        if (item[3] == "btc" && lastItem == "eth")
                        {
                            isExist_btc = true;
                        }
                        else if(item[3] == "eth" && lastItem == "btc")
                        {
                            isExist_eth = true;
                        }
                    }

                    var isExist = isExist_btc && isExist_eth;
                    Console.WriteLine($"[{filePath}] {isExist}");

                    if (isExist == false)
                    {
                        var strList = filePath.Split(@"\").ToList();
                        var count = strList.Count;
                        var msg = $"[{strList[count-3]}-{strList[count-2]}-{strList[count-1]}]";
                        missingDataList.Add(msg);
                    }

                    #endregion

                    
                }
            }

            CSVHelper.SaveDataToCSV(@".\\CSV", "Missingdata", missingDataList);
            Console.WriteLine($"[End]");
            Console.ReadLine();
        }
    }
}
