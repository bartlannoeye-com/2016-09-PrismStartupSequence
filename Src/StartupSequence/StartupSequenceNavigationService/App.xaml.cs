using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Autofac;
using Microsoft.Practices.ServiceLocation;
using Prism.Autofac.Windows;
using Prism.Windows.Navigation;
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
            await FillNavigationQueueAsync();
            await LoadAppResources();

            // small hack since we can't extend Prism's INavigationService in our own app
            var extendedNavigationService = (INavigationServiceWithBootSequence) NavigationService;
            extendedNavigationService.ContinueBootSequence();
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationSettingsService>().As<IApplicationSettingsService>().SingleInstance();
            builder.RegisterType<FrameNavigationServiceWithBootSequence>().As<INavigationService>().SingleInstance();
            base.ConfigureContainer(builder);
        }

        /// <summary>
        /// Override the creation of the FrameNavigationService
        /// </summary>
        protected override INavigationService OnCreateNavigationService(IFrameFacade rootFrame)
        {
            return new FrameNavigationServiceWithBootSequence(rootFrame, GetPageType, SessionStateService);
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
            var extendedNavigationService = (INavigationServiceWithBootSequence)NavigationService;
            extendedNavigationService.ContinueBootSequence();

            // step 1: check initial setup
            if (!applicationSettingsService.IsConfigured())
            {
                extendedNavigationService.AddToBootSequence(PageTokens.SetupPage, null);
            }

            // step 2: check user logged in
            if (string.IsNullOrEmpty(applicationSettingsService.GetUser()))
            {
                extendedNavigationService.AddToBootSequence(PageTokens.LoginPage, null);
            }

            // step 3: actual main page
            extendedNavigationService.AddToBootSequence(PageTokens.MainPage, null);
        }
    }
}
