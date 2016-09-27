// Note: this is an extension of Prism's FrameNavigationService
// https://github.com/PrismLibrary/Prism

using System;
using System.Collections.Generic;
using Prism.Windows.AppModel;
using Prism.Windows.Navigation;

namespace StartupSequenceNavigationService.Services
{
    public class FrameNavigationServiceWithBootSequence : FrameNavigationService, INavigationServiceWithBootSequence
    {
        private static readonly Queue<SequenceItem> BootSequence = new Queue<SequenceItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameNavigationServiceWithBootSequence"/> class.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <param name="navigationResolver">The navigation resolver.</param>
        /// <param name="sessionStateService">The session state service.</param>
        public FrameNavigationServiceWithBootSequence(IFrameFacade frame, Func<string, Type> navigationResolver, ISessionStateService sessionStateService)
            : base(frame, navigationResolver, sessionStateService)
        {
        }

        /// <summary>
        /// Returns if the application is currently executing the boot sequence
        /// </summary>
        public bool InBootSequence { get; private set; }

        /// <summary>
        /// Add another page to the end of the boot sequence queue
        /// </summary>
        /// <param name="pageToken"></param>
        /// <param name="parameter"></param>
        public void AddToBootSequence(string pageToken, object parameter)
        {
            BootSequence.Enqueue(new SequenceItem
            {
                PageToken = pageToken,
                Parameter = parameter
            });
            if (BootSequence.Count > 0)
                InBootSequence = true;
        }

        /// <summary>
        /// Execute the next navigation step in the boot sequence
        /// </summary>
        /// <returns></returns>
        public bool ContinueBootSequence()
        {
            if (InBootSequence)
            {
                SequenceItem sequenceItem = BootSequence.Dequeue();
                if (BootSequence.Count == 0)
                    InBootSequence = false;

                bool navigated = Navigate(sequenceItem.PageToken, sequenceItem.Parameter);
                RemoveAllPages();
                return navigated;
            }

            return false;
        }

        private struct SequenceItem
        {
            public string PageToken { get; set; }
            public object Parameter { get; set; }
        }
    }
}
