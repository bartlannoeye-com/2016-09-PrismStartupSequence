namespace StartupSequence.Services
{
    internal interface IApplicationSettingsService
    {
        void Login(string username);
        string GetUser();
        void SetConfigurationCompleted();
        bool IsConfigured();
    }
}