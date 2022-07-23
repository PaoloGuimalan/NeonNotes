using NeonNotesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeonNotesOnline.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        //[Route("Login")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LoginProcess(LoginData lgdata)
        {
            return Content(String.Format("Email: {0} Password: {1}", lgdata.email, lgdata.password));
        }
    }
}