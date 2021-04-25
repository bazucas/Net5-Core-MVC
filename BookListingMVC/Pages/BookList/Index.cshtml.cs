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
    public class IndexModel : PageModel
    {
        public ApplicationDbContext Db { get; }

        public IndexModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            Books = await Db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await Db.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            } 
            else
            {
                Db.Book.Remove(book);
                await Db.SaveChangesAsync();

                return RedirectToPage("Index");
            }
        }
    }
}
