using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    public class createModel : PageModel
    {
        private readonly BulkyBookRazorDbContext dbContext;
        [BindProperty]
        public Category category { get; set; }

        public createModel(BulkyBookRazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            TempData["success"] = "Category Created Successfully";
            return RedirectToPage("Index");
        }
    }
}
