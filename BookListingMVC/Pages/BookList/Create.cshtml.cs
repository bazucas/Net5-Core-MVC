using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListingMVC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListingMVC.Pages.BookList
{
    public class CreateModel : PageModel
    {
        public ApplicationDbContext Db { get; }

        [BindProperty]
        public Book Book { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            Db = db;
        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await Db.Book.AddAsync(Book);
                await Db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
