using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using StartupSequence.Services;

namespace StartupSequence.ViewModels
{
    internal class SetupPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApplicationSettingsService _applicationSettingsService;

        public SetupPageViewModel(INavigationService navigationService, IApplicationSettingsService applicationSettingsService)
        {
            _navigationService = navigationService;
            _applicationSettingsService = applicationSettingsService;

            GoCommand = new DelegateCommand(OnGoClicked);
        }

        public DelegateCommand GoCommand { get; private set; }

        private void OnGoClicked()
        {
            _applicationSettingsService.SetConfigurationCompleted();
            _navigationService.Navigate(PageTokens.LoginPage, null);
        }
    }
}
