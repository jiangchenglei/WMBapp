using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WMBAPP.IDAO;

namespace WMBAPP.Factory
{
   public class DAOFactory
    {
        private static readonly string path = ConfigurationManager.AppSettings["DapperPath"];
        public DAOFactory() { }

        public static IUserDAO CreateUserDAL()
        {
            string pathName = path + ".UserDAO";
            return (IUserDAO)Assembly.Load(path).CreateInstance(pathName);
        }
    }
}
