using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OnSale.Common.Models;
using OnSale.Prism.ItemViewModels;
using OnSale.Prism.Views;
using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class OnSaleMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public OnSaleMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
        {
            new Menu
            {
                Icon = "ic_card_giftcard",
                PageName = $"{nameof(ProductsPage)}",
                Title = "Products"
            },
            new Menu
            {
                Icon = "ic_shopping_cart",
                PageName = $"{nameof(ShowCarPage)}",
                Title = "ShowShoppingCar"
            },
            new Menu
            {
                Icon = "ic_history",
                PageName = $"{nameof(ShowHistoryPage)}",
                Title = "ShowPurchaseHistory",
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_person",
                PageName = $"{nameof(ModifyUserPage)}",
                Title = "ModifyUser",
                IsLoginRequired = true
            },
            new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = $"{nameof(LoginPage)}",
                Title = "Login"
            }
        };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title,
                    IsLoginRequired = m.IsLoginRequired
                }).ToList());
        }
    }
}