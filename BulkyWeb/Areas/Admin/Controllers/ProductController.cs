using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Repository;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Text.RegularExpressions;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            this._webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var objProductList = _UnitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _UnitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            //ViewBag.CategoryList = CategoryList;
            var productVM = new ProductVM()
            {
                CategoryList = _UnitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _UnitOfWork.productRepository.GetValue(u => u.ID == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                else
                {
                    productVM.Product.ImageUrl = @"\images\product\" + "NoImage.jpg";
                }

                    string description = productVM.Product.Description;
                string cleanString = Regex.Replace(description, "<.*?>", string.Empty);
                productVM.Product.Description = cleanString;

                if (productVM.Product.ID == 0)
                {
                    _UnitOfWork.productRepository.Add(productVM.Product);
                    TempData["Success"] = "Product Created Successfully";
                }
                else
                {
                    _UnitOfWork.productRepository.Update(productVM.Product);
                    TempData["Success"] = "Product Updated Successfully";
                }

                _UnitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _UnitOfWork.categoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        //public IActionResult Delete(int? id)
        //{
        //    var obj = _UnitOfWork.productRepository.GetValue(u => u.ID == id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}
        //[HttpPost]
        //public IActionResult Delete(Product obj)
        //{
        //    _UnitOfWork.productRepository.Remove(obj);
        //    _UnitOfWork.Save();
        //    TempData["success"] = "Product Deleted Successfully";
        //    return RedirectToAction("Index");

        //}

        //public IActionResult Edit(int? id)
        //{
        //    var obj = _UnitOfWork.productRepository.GetValue(u => u.ID == id);

        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _UnitOfWork.productRepository.Update(obj);
        //        _UnitOfWork.Save();
        //        TempData["Success"] = "Product Updated Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        #region API_CALLS
        [HttpGet]
        public IActionResult GetAllApi()
        {
            var objProductList = _UnitOfWork.productRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _UnitOfWork.productRepository.GetValue(u => u.ID == id);

            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            if (productToBeDeleted.ImageUrl.Contains("NoImage.jpg") == false)
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _UnitOfWork.productRepository.Remove(productToBeDeleted);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
