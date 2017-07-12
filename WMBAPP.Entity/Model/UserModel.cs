using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMBAPP.Entity.Model
{
    public class UserInquireModel
    {
        public UserInquireModel()
        {
            userList = new UserModel();
        }
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public UserModel userList { get; set; }
    }
    public class UserModel
    {
        public int ID { get; set; }

        public string UserAccount { get; set; }

        public string UserPassword { get; set; }

        public bool Status { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public string Remark { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
