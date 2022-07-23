using NeonNotesOnline.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

            List<Register> accountStore = new List<Register>();

            using (DBModel getAccount = new DBModel())
            {
                var data = getAccount.AccountsTables.Where(x => x.email == lgdata.email && x.password == lgdata.password).FirstOrDefault();
                if (data != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            //return Content(String.Format("Email: {0} Password: {1}", lgdata.email, lgdata.password));
        }

        public ActionResult RegisterProcess(Register regdata)
        {
            regdata.userName = userNameGenerator(regdata.lastName);

            var newAccountTable = new AccountsTable()
            {
                userName = regdata.userName,
                firstName = regdata.firstName,
                lastName = regdata.lastName,
                email = regdata.email,
                password = regdata.password
            };

            try
            {
                using (DBModel newAccount = new DBModel())
                {
                    newAccount.AccountsTables.Add(newAccountTable);
                    newAccount.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Register");
                //return Content("Error!"+ ex.Message);
            }
            //return Content(String.Format("Firstname: {0} Lastname: {1} Email: {2} Password: {3}", regdata.firstName, regdata.lastName, regdata.email, regdata.password));
        }

        public string userNameGenerator(string fname)
        {
            Random rnd = new Random();
            string userNameTest = String.Format("{0}_{1}", fname , rnd.Next(1000, 9999).ToString());
            string refurbishString = userNameTest.Replace(" ", "");
            return refurbishString;
        }
    }
}