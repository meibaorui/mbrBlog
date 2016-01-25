using mbrBlog.Code;
using mbrBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mbrBlog.Controllers
{
    public class AccountController : Controller
    {
        FormsAuthProvider authProvider = new FormsAuthProvider();
        //
        // GET: /Account/

        public ActionResult Login(LoginVM model , string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else 
                {
                    ModelState.AddModelError("", "用户名或密码不正确");
                    return View();
                }
            }
            else 
            {
                return View();
            }
            
        }

    }
}
