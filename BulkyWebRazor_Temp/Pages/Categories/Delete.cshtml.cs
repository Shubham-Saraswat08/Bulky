using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly BulkyBookRazorDbContext dbContext;
        public Category category { get; set; }

        public DeleteModel(BulkyBookRazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if(id != 0 || id != null)
            {
                category = dbContext.Categories.FirstOrDefault(u => u.Id == id);
            }
        }

        public IActionResult OnPost()
        {
            if (category != null)
            {
                dbContext.Categories.Remove(category);
                dbContext.SaveChanges();
                TempData["success"] = "Category Deleted Successfully";
                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
