using System;
using System.ComponentModel.DataAnnotations;

namespace OnSale.Common.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public Product Product { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
          //? "noimage"//null
           ? $"https://localhost:44312/images/Products/noimage.png"
        //: $"http://keypress.serveftp.net:88/OnSaleApi{ImageUrl.Substring(1)}";
        : $"https://localhost:44312{ImageUrl.Substring(1)}";
    }
}