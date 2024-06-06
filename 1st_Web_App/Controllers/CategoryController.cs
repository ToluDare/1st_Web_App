
using Microsoft.AspNetCore.Mvc;
using Web_App.Data;
using Web_App.DataAccess.Repository.IRepository;
using Web_App.Models;

namespace Web_App.DataAccess.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICartRepository _cartRepo;


        public CategoryController(ICategoryRepository db, ICartRepository cartRepo)
        {
            _categoryRepo = db;
            _cartRepo = cartRepo;
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

        public IActionResult AddToCart(int id) //getter
        {

            if (id == null || id == 0) //id of the row in which the add-to-cart button is clicked
            {
                return NotFound();
            }

            Category? categeryFromDb1 = _categoryRepo.Get(u => u.Id == id); // saves the id and the features allocated wit it

            if (categeryFromDb1 == null)
            {
                return NotFound();
            }
            var addToCartViewModel = new AddToCartViewModel { // creates a viewmodel that temporarily stores the id 
            CategoryStockQuantityId = id,
            };
            return View(addToCartViewModel);

        }
        [HttpPost]
        public IActionResult AddToCart(AddToCartViewModel obj) //sets , has the previous vew model as a parameter
        {
            if (ModelState.IsValid)
            {
                Category? categeryFromDb1 = _categoryRepo.Get(u => u.Id == obj.CategoryStockQuantityId); // collecting the values of the id 

                if (categeryFromDb1 == null)
                {
                    return NotFound();
                }
                var result = categeryFromDb1.StockQuantity - obj.Quantity; // values - user's input value
                categeryFromDb1.StockQuantity = result;

                _categoryRepo.Update(categeryFromDb1);
                _categoryRepo.Save();
                var cart = new Cart
                {
                    StockQuantity= obj.Quantity,
                    CategoryId = obj.CategoryStockQuantityId,
                    Name = categeryFromDb1.Name
                   
                };
               _cartRepo.Add(cart);
                _cartRepo.Save();

                TempData["success"] = "Addedd to Cart Successfully";
                return RedirectToAction("Index");
            }

            return View();

        }
    }
}
