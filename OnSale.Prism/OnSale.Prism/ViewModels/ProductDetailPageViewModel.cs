using Newtonsoft.Json;
using OnSale.Common.Entities;
using OnSale.Common.Helpers;
using OnSale.Common.Responses;
using OnSale.Prism.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace OnSale.Prism.ViewModels
{
    public class ProductDetailPageViewModel : ViewModelBase
    {
     

        public ProductDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Detail Product";
            _navigationService = navigationService;
        }

        private ProductResponse _product;

        private DelegateCommand _addToCartCommand;
        public DelegateCommand AddToCartCommand => _addToCartCommand ?? (_addToCartCommand = new DelegateCommand(AddToCartAsync));

        public ProductResponse Product
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
        private readonly INavigationService _navigationService;

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                Product = parameters.GetValue<ProductResponse>("product");
                Title = Product.Name;
                Images = new ObservableCollection<ProductImage>(Product.ProductImages);
            }
        }

        private async void AddToCartAsync()
        {
            NavigationParameters parameters = new NavigationParameters
                {
                    { "product", Product }
                };

            Settings.Product = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(AddToCartPage), parameters);
        }
    }
}