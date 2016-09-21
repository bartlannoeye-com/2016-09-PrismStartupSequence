using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Autofac;
using Microsoft.Practices.ServiceLocation;
using Prism.Autofac.Windows;
using StartupSequence.Services;
using StartupSequence.Views;

namespace StartupSequence
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismAutofacApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            ExtendedSplashScreenFactory = (splashscreen) => new ExtendedSplashScreen(splashscreen);
        }

        protected override async Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // Here we would load the application's resources.
            }

            await FillNavigationQueueAsync();
            await LoadAppResources();

            //NavigationService.Navigate(PageTokens.SetupPage, null);
            NavigationService.Navigate(StartupService.GetFromBootSequence(), null);
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationSettingsService>().As<IApplicationSettingsService>().SingleInstance();
            base.ConfigureContainer(builder);
        }

        /// <summary>
        /// We use this method to simulate the loading of resources from different sources asynchronously.
        /// </summary>
        /// <returns></returns>
        private Task LoadAppResources()
        {
            return Task.Delay(1000);
        }

        private async Task FillNavigationQueueAsync()
        {
            // do some async tasks to check the startup logic


            var applicationSettingsService = ServiceLocator.Current.GetInstance<IApplicationSettingsService>();

            // step 1: check initial setup
            if (!applicationSettingsService.IsConfigured())
            {
                StartupService.AddToBootSequence(PageTokens.SetupPage);
            }

            // step 2: check user logged in
            if (string.IsNullOrEmpty(applicationSettingsService.GetUser()))
            {
                StartupService.AddToBootSequence(PageTokens.LoginPage);
            }

            // step 3: actual main page
            StartupService.AddToBootSequence(PageTokens.MainPage);
        }
    }
}
