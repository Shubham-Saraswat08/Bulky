using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Models.ViewModels;
using System.Security.Claims;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.productRepository.GetAll(includeProperties: "Category");
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var product = _unitOfWork.productRepository.GetValue(u => u.ID == id, includeProperties: "Category");

            var addToCart = new AddToCartModel()
            {
                productID = id,
                Count = 1,
                UserID = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            var detailVM = new DetailVM()
            {
                Product = product,
                AddToCart = addToCart
            };
            return View(detailVM);
        }
        [HttpPost]
        public IActionResult Details(DetailVM detailVM)
        {
            var AddToCart = detailVM.AddToCart;
            if (AddToCart.UserID != null)
            {
                if (_unitOfWork.AddToCart.UpdateCheck(AddToCart))
                {
                    var data = _unitOfWork.AddToCart.GetValue(x => x.productID == AddToCart.productID && x.UserID == AddToCart.UserID);
                    if (data != null)
                    {
                        data.Count = AddToCart.Count;
                        _unitOfWork.AddToCart.Update(data);
                    }
                }
                else
                {
                    _unitOfWork.AddToCart.Add(AddToCart);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("AddToCart");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddToCart()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                var ListAddToCart = _unitOfWork.AddToCart.GetAll(includeProperties: "Product").Where(x => x.UserID == userID.ToString());
                return View(ListAddToCart);
            }
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Delete(int id)
        {
            var data = _unitOfWork.AddToCart.GetValue(x => x.id == id, "Product");
            if(data != null)
            {
                _unitOfWork.AddToCart.Remove(data);
                _unitOfWork.Save();
            }
            TempData["success"] = "Item Successfully Removed From Cart";
            return RedirectToAction("AddToCart");
        }
    }
}
