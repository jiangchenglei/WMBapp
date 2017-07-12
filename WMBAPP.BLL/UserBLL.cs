using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMBAPP.Entity.Model;
using WMBAPP.Factory;
using WMBAPP.IBLL;
using WMBAPP.IDAO;
using WMBAPP.Utility.Security;

namespace WMBAPP.BLL
{
    public class UserBLL: IUserBLL
    {
        private static readonly IUserDAO _userDal = DAOFactory.CreateUserDAL();

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        public void UserList(UserInquireModel inModel)
        {
            if (inModel!=null&&inModel.UserID>0)
            {
                 _userDal.UserList(inModel);
            }
        }
    }
}
