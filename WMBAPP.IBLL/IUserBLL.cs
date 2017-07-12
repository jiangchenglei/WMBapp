using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMBAPP.Entity.Model;

namespace WMBAPP.IBLL
{
    public interface IUserBLL
    {
        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="inModel"></param>
        /// <returns></returns>
        void UserList(UserInquireModel inModel);
    }
}
