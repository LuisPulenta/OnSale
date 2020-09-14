using OnSale.Common.Requests;
using OnSale.Common.Responses;
using OnSale.Common.Services;
using OnSale.Prism.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace OnSale.Prism.ViewModels
{
    public class RecoverPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IRegexHelper _regexHelper;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _recoverCommand;
        private string _email;

        public RecoverPasswordPageViewModel(
            INavigationService navigationService,
            IApiService apiService,
            IRegexHelper regexHelper)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _regexHelper = regexHelper;
            Title = "Recover Password";
            IsEnabled = true;
        }

        public DelegateCommand RecoverCommand => _recoverCommand ?? (_recoverCommand = new DelegateCommand(RecoverAsync));

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(parameters.ContainsKey("email"))
            {
                Email = parameters.GetValue<string>("email");
            }
        }




        private async void RecoverAsync()
        {
            bool isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Connection Error", "Accept");
                return;
            }

            EmailRequest request = new EmailRequest { Email = Email };
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.RecoverPasswordAsync(url, "api", "/Account/RecoverPassword", request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                if (response.Message == "Error001")
                {
                    await App.Current.MainPage.DisplayAlert("Error", "El Usuario no existe", "Accept");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                }

                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Recover Password Message", "Accept");
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrEmpty(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email Error", "Accept");
                return false;
            }

            return true;
        }
    }
}