using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly BulkyBookRazorDbContext dbContext;
        public Category category { get; set; }
        public EditModel(BulkyBookRazorDbContext dbContext)
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
