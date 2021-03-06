﻿using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using StartupSequenceNavigationService.Services;

namespace StartupSequenceNavigationService.ViewModels
{
    internal class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationServiceWithBootSequence _navigationService;
        private readonly IApplicationSettingsService _applicationSettingsService;

        public LoginPageViewModel(INavigationService navigationService, IApplicationSettingsService applicationSettingsService)
        {
            _navigationService = (INavigationServiceWithBootSequence)navigationService;
            _applicationSettingsService = applicationSettingsService;

            LoginCommand = new DelegateCommand(OnLoginClicked);
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public DelegateCommand LoginCommand { get; private set; }

        private void OnLoginClicked()
        {
            _applicationSettingsService.Login(Username);
            _navigationService.ContinueBootSequence();
        }
    }
}
