using Prism.Navigation;

namespace OnSale.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        public ModifyUserPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "ModifyUser";
        }
    }
}