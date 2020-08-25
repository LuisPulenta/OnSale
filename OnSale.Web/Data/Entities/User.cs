using Microsoft.AspNetCore.Identity;
using OnSale.Common.Entities;
using OnSale.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnSale.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        //TODO: Pending to put the correct paths
        [Display(Name = "Image")]
        public string ImageFullPath => string.IsNullOrEmpty(ImagePath)
            ? $"http://keypress.serveftp.net:88/OnSaleApi/images/Users/nouser.png"
           : $"http://keypress.serveftp.net:88/OnSaleApi{ImagePath.Substring(1)}";

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public City City { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}