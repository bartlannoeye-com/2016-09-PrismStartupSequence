using System.Threading.Tasks;

namespace StartupSequence.Services
{
    internal class ApplicationSettingsService : IApplicationSettingsService
    {
        private string _username;
        private bool _isConfigurationComplete;

        public void Login(string username)
        {
            _username = username;
        }

        public string GetUser()
        {
            return _username;
        }

        public void SetConfigurationCompleted()
        {
            _isConfigurationComplete = true;
        }

        public bool IsConfigured()
        {
            return _isConfigurationComplete;
        }
    }
}
