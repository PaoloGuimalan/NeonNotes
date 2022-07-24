using NeonNotesOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeonNotesOnline.Controllers
{
    public class HomeController : Controller
    {

        public bool LoginChecker()
        {
            HttpCookie cookiefetch = Request.Cookies["loginID"];

            if (cookiefetch != null)
            {
                string cookieID = cookiefetch.Value.Split('=')[1];

                using (DBModel checkAccount = new DBModel())
                {
                    var userResult = checkAccount.AccountsTables.Where(x => x.userName == cookieID).FirstOrDefault();
                    if (userResult != null)
                    {
                        LoginStatusCreds.status = true;
                        LoginStatusCreds.loginIDCred = userResult.userName.ToString();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public ActionResult LogoutData()
        {
            if (Request.Cookies["loginID"] != null)
            {
                var c = new HttpCookie("loginID");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToActionPermanent("Index", "Auth");

            //return Content("Hello");
        }

        // GET: Home
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            HttpCookie cookiefetch = Request.Cookies["loginID"];

            ViewBag.PageTitle = "Neon Notes | Home";
            if (LoginChecker())
            {
                string cookieID = cookiefetch.Value.Split('=')[1];

                using (DBModel getNotes = new DBModel())
                {
                    HomeViewModel datalist = new HomeViewModel()
                    {
                        ExistingNotesHolder = getNotes.NotesTables.Where(x => x.username == cookieID).ToList()
                    };

                    return View(datalist);
                    /*var datacheck = getNotes.NotesTables.Where(x => x.username == cookieID).ToList();

                    if (datacheck == null)
                    {
                        return Content("true");
                    }
                    else
                    {
                        return Content(String.Format("false: {0}", datacheck[0].username));
                    }*/
                }

                //return View();

            }
            else
            {
                return RedirectToActionPermanent("Index", "Auth");
            }
        }

        //[HttpPost]
        public ActionResult DeleteNote(int id)
        {
            if (LoginChecker())
            {
                using (DBModel deleteCmd = new DBModel())
                {
                    var noteID = new NotesTable() { id = id };
                    deleteCmd.NotesTables.Attach(noteID);
                    deleteCmd.NotesTables.Remove(noteID);
                    deleteCmd.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            //return Content(String.Format("Hello {0}", id));
        }

        [HttpPost]
        public ActionResult AddNote(HomeViewModel addedNotes)
        {
            if (LoginChecker())
            {
                var newNotes = new NotesTable()
                {
                    username = LoginStatusCreds.loginIDCred,
                    notesSubject = addedNotes.AddNotesHolder.notesSubject,
                    notesContent = addedNotes.AddNotesHolder.notesContent,
                    dateMade = DateTime.Now
                };

                try
                {
                    using (DBModel adder = new DBModel())
                    {
                        adder.NotesTables.Add(newNotes);
                        adder.SaveChanges();
                    }
                }
                catch
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
                //return Content(String.Format("Username: {0}, Subject: {1}, Content: {2}, Date: {3}", LoginStatusCreds.loginIDCred, addedNotes.AddNotesHolder.notesSubject, addedNotes.AddNotesHolder.notesContent, addedNotes.AddNotesHolder.notesDateMade));
            }
            else
            {
                return Content("Not Logged In!");
            }
        }

        public ActionResult Notes()
        {
            /* NotesList newNotes = new NotesList()
             {
                 notesID = 1,
                 notesOwner = "Paolo_187",
                 notesContent = "This is a test Note!",
                 notesDateMade = DateTime.Now
             };
            */

            List<NotesList> newNotes = new List<NotesList>()
            {
                new NotesList()
                {
                    notesID = 1,
                    notesSubject = "Test",
                    notesOwner = "Paolo_187",
                    notesContent = "This is a test Note!",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 2,
                    notesSubject = "Test",
                    notesOwner = "Juan_187",
                    notesContent = "notes of Juan!",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 3,
                    notesSubject = "Test",
                    notesOwner = "Paolo_187",
                    notesContent = "Just another Note for the Loop",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 4,
                    notesSubject = "Test",
                    notesOwner = "Juan_187",
                    notesContent = "Last One, or should I say last Juan! haha corny.",
                    notesDateMade = DateTime.Now
                }
            };

            ViewBag.PageTitle = "Neon Notes | Your Notes";
            ViewBag.UserNotes = newNotes;

            var NotesArrayVar = new NotesArray()
            {
                Lister = newNotes
            };

            ViewBag.NotesArrayVar = NotesArrayVar;

            return View(NotesArrayVar);
        }

        //custom route, can declare parameters with datatype auth example /whatsnew/{id:int}/{name:string}
        //can also have method declaration in parameters like range() /whatsnew/{id:int:range(1,12)}/{name:string}
        [Route("about/whatsnew")]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult About()
        {
            if (LoginChecker())
            {
                ViewData["PageTitle"] = "About Neon";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Auth");
            }
        }
    }
}