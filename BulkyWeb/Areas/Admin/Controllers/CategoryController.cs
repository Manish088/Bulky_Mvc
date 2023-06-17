using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using BulkyWeb.Areas.Customer.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category obj)
        {
            try
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "The DisplayOrders Cannot exactly match the Name.");
                }
                /*if (obj.Name != null && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("", "The Test is an invalid Value.");
                }*/
                if (ModelState.IsValid)
                {
                    _unitOfWork.Category.Add(obj);
                    _unitOfWork.save();
                    TempData["Success"] = "category Created SuccessFully";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u=>u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category obj)
        {
            try
            {
                if(obj==null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _unitOfWork.Category.Update(obj);
                    _unitOfWork.save();
                    TempData["Success"] = "category Update SuccessFully";
                    return RedirectToAction(nameof(Index));
                }
                return View();
                
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        // POST: CategoryController/Delete/5
        [HttpPost,ActionName("Delete")]
        
        public ActionResult DeletePost(int? id)
        {
            try
            {
                Category? obj= _unitOfWork.Category.Get(u => u.Id == id);
                if (obj==null)
                {
                    return NotFound();
                }
                _unitOfWork.Category.Remove(obj);
                _unitOfWork.save();
                TempData["Success"] = "category Delete SuccessFully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
