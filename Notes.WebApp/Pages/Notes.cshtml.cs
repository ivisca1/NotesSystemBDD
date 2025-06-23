using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notes.WebApp.Models;

namespace Notes.WebApp.Pages
{
    public class NotesModel : PageModel
    {
        private static readonly List<Note> _notes = new();

        [BindProperty]
        public CreateNoteDto NewNote { get; set; }

        public List<Note> Notes => _notes;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = NewNote.Title,
                Content = NewNote.Content
            };

            _notes.Add(note);

            return RedirectToPage();
        }
    }
}
