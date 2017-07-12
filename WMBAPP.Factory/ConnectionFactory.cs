using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMBAPP.Factory
{
    /// <summary>
    /// Connection工厂用于实例化对应的IDbConnection对象，传递给Dapper。
    /// </summary>
    public class ConnectionFactory
    {
        private static readonly string connectionName = ConfigurationManager.AppSettings["ConnectionName"];
        private static readonly string connectionString = ConfigurationManager.AppSettings["DbConnString"].ToString();

        public static IDbConnection CreateConnection()
        {
            IDbConnection conn = null;
            switch (connectionName)
            {
                case "SQLServer":
                    conn = new SqlConnection(connectionString);
                    break;
                case "MySQL":
                    conn = new MySqlConnection(connectionString);
                    break;
                default:
                    conn = new SqlConnection(connectionString);
                    break;
            }
            conn.Open();
            return conn;
        }
    }
}
