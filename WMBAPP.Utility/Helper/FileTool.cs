using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bill.Utility.Helper
{
    public class FileTool
    {
        /// <summary> 
        /// 写入日志文本文件 
        /// </summary> 
        /// <param name="filePath"></param> 
        /// <param name="savePath"></param> 
        /// <param name="log"></param> 
        public static void WriteLog(string filePath, string savePath, string log)
        {
            try
            {
                string logFilePath = filePath + savePath;
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                using (StreamWriter sw = new StreamWriter(logFilePath + string.Format("{0:yyyy-MM-dd}.txt", DateTime.Now), true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(string.Format("{0} LOG：{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), log));
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch
            {

            }
        }
    }
}
