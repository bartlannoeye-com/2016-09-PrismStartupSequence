// Note: this is an extension of Prism's INavigationServic
// https://github.com/PrismLibrary/Prism

using Prism.Windows.Navigation;

namespace StartupSequenceNavigationService.Services
{
    /// <summary>
    /// The INavigationService interface is used for creating a navigation service for your Windows Store app.
    /// The default implementation of INavigationService is the FrameNavigationService class, that uses a class that implements the IFrameFacade interface
    /// to provide page navigation.
    /// </summary>
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
