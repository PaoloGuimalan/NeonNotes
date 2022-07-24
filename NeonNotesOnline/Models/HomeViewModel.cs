using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeonNotesOnline.Models
{
    public class HomeViewModel
    {
        public NotesList AddNotesHolder { get; set; }
        public IEnumerable<NotesTable> ExistingNotesHolder { get; set; }
    }
}