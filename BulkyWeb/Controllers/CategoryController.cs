using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    _db.Categories.Add(obj);
                    _db.SaveChanges();
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

            Category? categoryFromDb = _db.Categories.Find(id);

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
                    _db.Categories.Update(obj);
                    _db.SaveChanges();
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

            Category? categoryFromDb = _db.Categories.Find(id);

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
                Category? obj=_db.Categories.Find(id);
                if(obj==null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
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
