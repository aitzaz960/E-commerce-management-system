using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int approvingId { get; set; }

        public string status { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Required")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ShippingCharges { get; set; }
        
        public int Rating { get; set; }
        public String PicTitle { get; set; }
        [NotMapped]
        [Display(Name = "Product Picture")]
        public IFormFile PictureFile { get; set; }

    }
}
