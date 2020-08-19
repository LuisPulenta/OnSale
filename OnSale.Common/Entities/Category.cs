﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnSale.Common.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characters")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }


        public ICollection<Product> Products { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => string.IsNullOrEmpty(ImagePath)
           ? $"https://localhost:44312/images/Categories/noimage.png"
           : $"http://keypress.serveftp.net:88/OnSaleApi{ImagePath.Substring(1)}";
    }
}
