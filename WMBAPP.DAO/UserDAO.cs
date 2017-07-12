using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMBAPP.Entity.Model;
using WMBAPP.Factory;
using WMBAPP.IDAO;

namespace WMBAPP.DAO
{
    public class UserDAO: IUserDAO
    {
        private IDbConnection _conn;
        public IDbConnection Conn
        {
            get
            {
                return _conn = ConnectionFactory.CreateConnection();
            }
        }

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public void UserList(UserInquireModel inModel)
        {
            const string SqlStr = @"select ID=ID_int,
                                           UserAccount = UserAccount_nvarchar,
	                                       UserPassword = UserPassword_nvarchar,
                                           [Status]=Status_bit,
	                                       LastLoginTime=LastLoginTime_datetime,
	                                       Remark=Remark_nvarchar,
	                                       CreateTime=CreateTime_datetime
                                    from[dbo].[User]
                                            where ID_int = @id";
            using (Conn)
            {
                inModel.userList = Conn.Query<UserModel>(SqlStr, new { id = inModel.UserID }).FirstOrDefault();
            }
        }
    }
}
