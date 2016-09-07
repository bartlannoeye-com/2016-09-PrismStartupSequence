using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StartupSequence.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplashScreen : Page
    {
        private readonly SplashScreen _splashScreen;

        public ExtendedSplashScreen(SplashScreen splashScreen)
        {
            _splashScreen = splashScreen;

            InitializeComponent();

            SizeChanged += ExtendedSplashScreen_SizeChanged;
            SplashImage.ImageOpened += splashImage_ImageOpened;
        }

        private void splashImage_ImageOpened(object sender, RoutedEventArgs e)
        {
            // The application's window should not become activate until the extended splash screen is ready to be shown 
            // in order to prevent flickering when switching between the real splash screen and this one.
            // In order to do this we need to be sure that the image was opened so we subscribed to
            // this event and activate the window in it.

            Resize();
            Window.Current.Activate();
        }

        // Whenever the size of the application change, the image position and size need to be recalculated.
        private void ExtendedSplashScreen_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize();
        }

        // This method is used to position and resizing the splash screen image correctly.
        private void Resize()
        {
            if (_splashScreen == null) return;

            // The splash image's not always perfectly centered. Therefore we need to set our image's position 
            // to match the original one to obtain a clean transition between both splash screens.

            SplashImage.Height = _splashScreen.ImageLocation.Height;
            SplashImage.Width = _splashScreen.ImageLocation.Width;

            SplashImage.SetValue(Canvas.TopProperty, _splashScreen.ImageLocation.Top);
            SplashImage.SetValue(Canvas.LeftProperty, _splashScreen.ImageLocation.Left);

            ProgressRing.SetValue(Canvas.TopProperty, _splashScreen.ImageLocation.Top + _splashScreen.ImageLocation.Height + 50);
            ProgressRing.SetValue(Canvas.LeftProperty, _splashScreen.ImageLocation.Left + _splashScreen.ImageLocation.Width / 2 - ProgressRing.Width / 2);
        }
    }
}
