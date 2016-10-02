// Note: this is an extension of Prism's INavigationService
// https://github.com/PrismLibrary/Prism

using Prism.Windows.Navigation;

namespace StartupSequenceNavigationService.Services
{
    public interface INavigationServiceWithBootSequence : INavigationService
    {
        /// <summary>
        /// Returns if the application is currently executing the boot sequence
        /// </summary>
        bool InBootSequence { get; }

        /// <summary>
        /// Add another page to the end of the boot sequence queue
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="parameter"></param>
        void AddToBootSequence(string pageToken, object parameter);

        /// <summary>
        /// Execute the next navigation step in the boot sequence
        /// </summary>
        /// <returns></returns>
        bool ContinueBootSequence();
    }
}
