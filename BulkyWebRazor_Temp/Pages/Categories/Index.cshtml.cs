using BulkyBookWebRazor_Temp.Data;
using BulkyBookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly BulkyBookRazorDbContext dbContext;
        public List<Category> categoryList { get; set; }

        public IndexModel(BulkyBookRazorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
            categoryList = dbContext.Categories.ToList();
        }
    }
}
