
using Microsoft.AspNetCore.Mvc;
using Web_App.Data;
using Web_App.DataAccess.Repository.IRepository;
using Web_App.Models;

namespace Web_App.DataAccess.Controllers
{
    public class CartController : Controller //controller for the whole cart
    {
        private readonly ICartRepository _cartRepo;//edited

        public CartController(ICartRepository db) {//edited - creating the database
            _cartRepo = db;
        }
        public IActionResult Index()// to view the full cart
        {
            List<Cart> objCartList = _cartRepo.GetAll().ToList();//edited
            return View(objCartList);
        }
        /*
        public IActionResult Create()// i might not need this
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) //or this
        {
            if (ModelState.IsValid)
            {
                _cartRepo.Add(obj);
               // _cartRepo.Save();
                TempData["success"] = "Cart Item Created Successfully";//edited
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {

            if(id==null|| id == 0) {
                return NotFound();
            }
            
            Category? categeryFromDb1 = _cartRepo.Get(u=> u.Id ==id);
           
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

        }*/
        public IActionResult Delete(int id) // helper function - to find what is to be deleted
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
  
            Cart? cartFromDb1 = _cartRepo.Get(u => u.Id == id);
            
            if (cartFromDb1 == null)
            {
                return NotFound();
            }
            return View(cartFromDb1);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) // to actually delete

        {
            Cart? obj = _cartRepo.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _cartRepo.Remove(obj);
            _cartRepo.Save();
            TempData["success"] = "Cart Item Deleted Successfully";
            return RedirectToAction("Index");

            
        }

        

        //save function?
    }
}
