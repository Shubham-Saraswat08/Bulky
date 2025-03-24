using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class AddToCartModel
    {
        [Key]
        public int id { get; set; }

        public int productID { get; set; }

        public int Count { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("productID")]
        [ValidateNever]
        public Product Product { get; set; }
    }
}
