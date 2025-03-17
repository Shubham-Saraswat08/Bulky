using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly BulkyRazorDbContext dbContext;
        public Category category { get; set; }
        public EditModel(BulkyRazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                category = dbContext.Categories.FirstOrDefault(u => u.Id == id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (category != null)
                {
                    dbContext.Categories.Update(category);
                    dbContext.SaveChanges();
                    TempData["success"] = "Category Edited Successfully";
                    return RedirectToPage("index");
                }
            }
            return Page();
        }
    }
}
