using Microsoft.AspNetCore.Mvc;
using Bucket_list_mvc.Models;
using System.Linq;

namespace Bucket_list_mvc.Controllers
{
    public class HomeController : Controller
    {
        // We replace the 'static List' with the Database Context
        private readonly AppDbContext _context;

        // This "Constructor" tells the app to use the SQLite database
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch items from the SQLite database file (.db)
            var items = _context.BucketItems.OrderByDescending(x => x.DateCreated).ToList();
            return View(items);
        }

        [HttpPost]
        public IActionResult Create(string ActivityName)
        {
            if (!string.IsNullOrWhiteSpace(ActivityName))
            {
                // Add the new item to the database table
                _context.BucketItems.Add(new BucketItem 
                { 
                    ActivityName = ActivityName,
                    DateCreated = DateTime.Now 
                });
                _context.SaveChanges(); // This saves it to the .db file
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _context.BucketItems.Find(id);
            if (item != null) 
            {
                item.IsCompleted = !item.IsCompleted;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _context.BucketItems.Find(id);
            if (item != null) 
            {
                _context.BucketItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
