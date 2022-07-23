using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeonNotesOnline.Models
{
    public class NotesList
    {
        public int notesID { get; set; }
        public string notesOwner { get; set; }
        public string notesSubject { get; set; }
        public string notesContent { get; set; }
        public System.DateTime notesDateMade { get; set; }

    }
}