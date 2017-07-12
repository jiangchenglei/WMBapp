using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMBAPP.BLL;
using WMBAPP.Entity.Model;
using WMBAPP.IBLL;

namespace WMBAPP.WEB.Controllers
{
    public class HomeController : Controller
    {
        IUserBLL _Userbll = new UserBLL();
        public ActionResult Index(UserInquireModel inModel)
        {
            inModel.UserID = 1;
            _Userbll.UserList(inModel);
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
