using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;

namespace MyIBB.Controllers
{
    public class AccountController : Controller
    {
        private ILoginRepository loginRepsitory;
        MyIBBContext db = new MyIBBContext();
        public AccountController()
        {
            loginRepsitory = new LoginRepository(db);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                if (loginRepsitory.IsExistUser(login.UserName, login.Password))
                {

                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری یافت نشد");
                }
            }
            return View(login);
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}