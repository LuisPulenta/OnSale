using Prism;
using Prism.Ioc;

using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using OnSale.Common.Services;
using OnSale.Prism.Views;
using OnSale.Prism.ViewModels;
using OnSale.Prism.Helpers;
using OnSale.Common.Helpers;

namespace OnSale.Prism
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY2MzIyQDMxMzcyZTMzMmUzMFVnNW5KSnM2dTZmRDljWm1RYTduQXFwRmNKSzVPWk1lT1JGSFRySXZCUTA9");
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(OnSaleMasterDetailPage)}/NavigationPage/{nameof(ProductsPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ProductsPage, ProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductDetailPage, ProductDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowCarPage, ShowCarPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowHistoryPage, ShowHistoryPageViewModel>();

            containerRegistry.RegisterForNavigation<OnSaleMasterDetailPage, OnSaleMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();


            containerRegistry.RegisterForNavigation<QualificationsPage, QualificationsPageViewModel>();
            containerRegistry.RegisterForNavigation<QualificationDetailPage, QualificationDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<AddQualificationPage, AddQualificationPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductTabbedPage, ProductTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RecoverPasswordPage, RecoverPasswordPageViewModel>();
        }
    }
}
