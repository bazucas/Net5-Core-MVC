using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListingMVC.Model;
using Microsoft.EntityFrameworkCore;

namespace BookListingMVC.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        public ApplicationDbContext Db { get; }

        public BookController(ApplicationDbContext db)
        {
            Db = db;
        }

        public IEnumerable<Book> Books { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new {data = await Db.Book.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await Db.Book.FirstOrDefaultAsync(b => b.Id == id);

            if (bookFromDb == null)
            {
                return Json(new {success = false, message = "Error while deleting book"});
            }

            Db.Remove(bookFromDb);
            await Db.SaveChangesAsync();
            return Json(new {success = true, message="Delete successful"});
        }
    }
}
