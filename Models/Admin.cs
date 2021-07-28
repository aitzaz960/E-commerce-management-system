using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Account_key { get; set; }
        public string First_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Last_Name { get; set; }

        public string Gender { get; set; }

        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

        public String Address { get; set; }

        public String PicTitle { get; set; }
        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile PictureFile { get; set; }
    }
}
