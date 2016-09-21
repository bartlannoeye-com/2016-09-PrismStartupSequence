using System.Collections.Generic;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using StartupSequence.Services;

namespace StartupSequence.ViewModels
{
    internal class MainPageViewModel : ViewModelBase
    {
        private readonly IApplicationSettingsService _applicationSettingsService;

        public MainPageViewModel(IApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;

            ClearSetupCommand = new DelegateCommand(() => { _applicationSettingsService.ClearSetup(); });
            LogoutCommand = new DelegateCommand(() => { _applicationSettingsService.Logout(); });
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public DelegateCommand ClearSetupCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var user = _applicationSettingsService.GetUser();
            Message = $"Welcome {user}";
        }
    }
}
