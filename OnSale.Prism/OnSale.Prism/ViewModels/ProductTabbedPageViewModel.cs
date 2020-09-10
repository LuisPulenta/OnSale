using OnSale.Common.Responses;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ProductTabbedPageViewModel : ViewModelBase
    {
        public ProductTabbedPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Product";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("product"))
            {
                ProductResponse product = parameters.GetValue<ProductResponse>("product");
                Title = product.Name;
            }
        }
    }
}