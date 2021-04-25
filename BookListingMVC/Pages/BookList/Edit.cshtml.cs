using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListingMVC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListingMVC.Pages.BookList
{
    public class EditModel : PageModel
    {
        public ApplicationDbContext Db { get; }

        [BindProperty]
        public Book Book { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public async Task OnGet(int id)
        {
            Book = await Db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDb = await Db.Book.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.Author = Book.Author;
                BookFromDb.Isbn = Book.Isbn;

                await Db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
