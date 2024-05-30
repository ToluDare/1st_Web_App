
using Microsoft.AspNetCore.Mvc;
using Web_App.Data;
using Web_App.DataAccess.Repository.IRepository;
using Web_App.Models;

namespace Web_App.DataAccess.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository db) {
            _categoryRepo = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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
            
            Category? categeryFromDb1 = _categoryRepo.Get(u=> u.Id ==id);
           
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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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
  
            Category? categeryFromDb1 = _categoryRepo.Get(u => u.Id == id);
            
            if (categeryFromDb1 == null)
            {
                return NotFound();
            }
            return View(categeryFromDb1);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)

        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

            
        }
    }
}
