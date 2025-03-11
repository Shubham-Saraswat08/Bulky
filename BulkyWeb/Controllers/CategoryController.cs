using BulkyWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BulkyWebDbContext dbContext;

        public CategoryController(BulkyWebDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var objCategoryList = dbContext.Categories.ToList();
            return View(objCategoryList);
        }
    }
}
