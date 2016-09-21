using System.Collections.Generic;

namespace StartupSequence.Services
{
    public static class StartupService
    {
        private static readonly Queue<string> BootSequence = new Queue<string>();            

        public static bool StartupRunning { get; set; }

        public static void AddToBootSequence(string view)
        {            
            BootSequence.Enqueue(view);
            if (BootSequence.Count > 0)
                StartupRunning = true;
        }

        public static string GetFromBootSequence()
        {
            string view = BootSequence.Dequeue();
            if (BootSequence.Count == 0)
                StartupRunning = false;

            return view;
        }
    }
}
