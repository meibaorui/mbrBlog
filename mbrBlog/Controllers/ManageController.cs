using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mbrBlog.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
