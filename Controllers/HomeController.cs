using Microsoft.AspNetCore.Mvc;
using Bucket_list_mvc.Models;

namespace Bucket_list_mvc.Controllers
{
    public class HomeController : Controller
    {
        private static List<BucketItem> _items = new List<BucketItem>();

        public IActionResult Index()
        {
            return View(_items.OrderByDescending(x => x.DateCreated).ToList());
        }

        [HttpPost]
        public IActionResult Create(string ActivityName)
        {
            if (!string.IsNullOrWhiteSpace(ActivityName))
            {
                _items.Add(new BucketItem { Id = _items.Count + 1, ActivityName = ActivityName });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null) item.IsCompleted = !item.IsCompleted;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _items.RemoveAll(x => x.Id == id);
            return RedirectToAction("Index");
        }
    }
}