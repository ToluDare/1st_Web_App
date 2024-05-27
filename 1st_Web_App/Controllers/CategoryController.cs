using _1st_Web_App.Data;
using _1st_Web_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace _1st_Web_App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {

            if(id==null|| id == 0) {
                return NotFound();
            }
            //Category? categeryFromDb = _db.Categories.Find(id);
            Category? categeryFromDb1 = _db.Categories.FirstOrDefault(u=> u.Id ==id);
            //Category? categeryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categeryFromDb1 == null)
            {
                return NotFound();
            }
             return View(categeryFromDb1);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Delete(int id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? categeryFromDb = _db.Categories.Find(id);
            Category? categeryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categeryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categeryFromDb1 == null)
            {
                return NotFound();
            }
            return View(categeryFromDb1);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)

        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

            
        }
    }
}
