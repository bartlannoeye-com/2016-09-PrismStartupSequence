using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Autofac;
using Prism.Autofac.Windows;
using StartupSequenceNavigationService.Services;
using StartupSequenceNavigationService.Views;

namespace StartupSequenceNavigationService
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
            await LoadAppResources();

            NavigationService.Navigate(PageTokens.SetupPage, null);
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
            return Task.Delay(5000);
        }
    }
}
