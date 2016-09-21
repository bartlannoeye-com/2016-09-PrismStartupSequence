using Windows.Storage;

namespace StartupSequence.Services
{
    /// <summary>
    /// This would probably do some async calls to local storage or the cloud
    /// Keeping it simple for sample purposes
    /// </summary>
    internal class ApplicationSettingsService : IApplicationSettingsService
    {
        private const string KeyUsername = "Username";
        private const string KeyConfiguration = "Configuration";

        public void Login(string username)
        {
            ApplicationData.Current.LocalSettings.Values[KeyUsername] = username;
        }

        public string GetUser()
        {
            return ApplicationData.Current.LocalSettings.Values[KeyUsername]?.ToString();
        }

        public void SetConfigurationCompleted()
        {
            ApplicationData.Current.LocalSettings.Values[KeyConfiguration] = true;
        }

        public bool IsConfigured()
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(KeyConfiguration))
                return (bool) ApplicationData.Current.LocalSettings.Values[KeyConfiguration];
            return false;
        }

        public void ClearSetup()
        {
            ApplicationData.Current.LocalSettings.Values[KeyConfiguration] = false;
        }

        public void Logout()
        {
            Login(null);
        }
    }
}
