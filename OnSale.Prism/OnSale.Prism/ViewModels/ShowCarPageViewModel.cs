﻿using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ShowCarPageViewModel : ViewModelBase
    {
        public ShowCarPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "ShowShoppingCar";
        }
    }
}