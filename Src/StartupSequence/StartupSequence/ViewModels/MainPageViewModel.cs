using System.Collections.Generic;
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
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var user = _applicationSettingsService.GetUser();
            Message = $"Welcome {user}";
        }
    }
}
