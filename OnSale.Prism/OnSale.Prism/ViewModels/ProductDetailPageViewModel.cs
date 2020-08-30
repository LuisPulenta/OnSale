﻿using OnSale.Common.Entities;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace OnSale.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
        public ProductDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Product";
        }

        private Product _product;

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }
        public ObservableCollection<ProductImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private ObservableCollection<ProductImage> _images;

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<Product>("product");
                Title = Product.Name;
                Images = new ObservableCollection<ProductImage>(Product.ProductImages);
            }
        }


    }

}