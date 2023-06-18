using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(objProductList);
        }



        // GET: ProductController/Create
        public ActionResult Upsert(int? id)
        {
            /*IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });*/
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"]=CategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id==null || id==0)
            {
                // Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        // POST: ProductController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upsert(ProductVM ProductVM,IFormFile? file)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if(file!=null)
                    {
                        string filename=Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productPath=Path.Combine(wwwRootPath,@"Images\Product");
                        if(!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                        {
                            //Delete the old image
                            var oldImagePath = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));

                            if(System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }
                        using (var fileStream =new FileStream(Path.Combine(productPath, filename),FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProductVM.Product.ImageUrl = @"\Images\product\" + filename;
                    }

                    if(ProductVM.Product.Id==0)
                    {

                        _unitOfWork.Product.Add(ProductVM.Product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(ProductVM.Product);

                    }
                    _unitOfWork.save();
                    TempData["Success"] = "product Created SuccessFully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    ProductVM.CategoryList = _unitOfWork.Category.GetAll()
                        .Select(u => new SelectListItem
                        {
                            Text = u.Name,
                            Value = u.Id.ToString()
                        });
                    return View(ProductVM);
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product obj)
        {
            try
            {
                if (obj == null)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    _unitOfWork.Product.Update(obj);
                    _unitOfWork.save();
                    TempData["Success"] = "product Update SuccessFully";
                    return RedirectToAction(nameof(Index));
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
       /* public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeletePost(int? id)
        {
            try
            {
                Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.save();
                TempData["Success"] = "product Delete SuccessFully";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
           var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new {success = false,message="Error while deleting"});
            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, 
                productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.save();
            return Json(new { success = false, message = "Delete SuccessFul" });
        }
        #endregion
    }
}
