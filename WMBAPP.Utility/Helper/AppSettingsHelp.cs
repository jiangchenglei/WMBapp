using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace WMB.Utility.Helper
{
    /// <summary>
    /// 操作webconfig的AppSettings的类
    /// </summary>
    public class AppSettingsHelp
    {
        /// <summary>
        /// 读取AppSettings
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            ConfigurationManager.RefreshSection("appSettings");
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }
    }
}
