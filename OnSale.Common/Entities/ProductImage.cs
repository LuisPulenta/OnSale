﻿using Newtonsoft.Json;
using OnSale.Common.Responses;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
          //? "noimage"//null
           ? $"http://keypress.serveftp.net:88/OnSaleApi/images/Products/noimage.png"
        //: $"http://keypress.serveftp.net:88/OnSaleApi{ImageUrl.Substring(1)}";
        : $"http://keypress.serveftp.net:88/OnSaleApi{ImageUrl.Substring(1)}";
    }
}