using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly BulkyRazorDbContext dbContext;
        public Category category { get; set; }

        public DeleteModel(BulkyRazorDbContext dbContext)
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
                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
