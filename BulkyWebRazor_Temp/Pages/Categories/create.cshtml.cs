using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    public class createModel : PageModel
    {
        private readonly BulkyRazorDbContext dbContext;
        [BindProperty]
        public Category category { get; set; }

        public createModel(BulkyRazorDbContext dbContext)
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
            return RedirectToPage("Index");
        }
    }
}
