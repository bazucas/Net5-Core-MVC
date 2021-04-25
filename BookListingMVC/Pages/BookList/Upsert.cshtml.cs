using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListingMVC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListingMVC.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        public ApplicationDbContext Db { get; }

        [BindProperty]
        public Book Book { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                // create
                return Page();
            }

            // update
            Book = await Db.Book.FirstOrDefaultAsync(b => b.Id == id);
            if (Book == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                //var BookFromDb = await Db.Book.FindAsync(Book.Id);
                //BookFromDb.Name = Book.Name;
                //BookFromDb.Author = Book.Author;
                //BookFromDb.Isbn = Book.Isbn;

                if (Book.Id == 0)
                {
                    Db.Book.Add(Book);
                }
                else
                {
                    Db.Book.Update(Book);
                }

                await Db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
