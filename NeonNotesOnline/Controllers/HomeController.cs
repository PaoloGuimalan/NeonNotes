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
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Neon Notes | Home";
            return View();
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
                    notesOwner = "Paolo_187",
                    notesContent = "This is a test Note!",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 2,
                    notesOwner = "Juan_187",
                    notesContent = "notes of Juan!",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 3,
                    notesOwner = "Paolo_187",
                    notesContent = "Just another Note for the Loop",
                    notesDateMade = DateTime.Now
                },
                new NotesList()
                {
                    notesID = 4,
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
        public ActionResult About()
        {
            ViewData["PageTitle"] = "About Neon";
            return View();
        }
    }
}