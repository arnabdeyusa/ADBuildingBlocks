using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.AsyncTask
{
    public class AppTaskService
    {
        public static void RunAppTask(IAppTask appTask)
        {
            // kickoff thread
            var originalPrincipal = Thread.CurrentPrincipal;

            Thread workerThread = new Thread(() =>
            {
                Thread.CurrentPrincipal = originalPrincipal;

                appTask.Run();

            });

            // kickoff the thread, log any result somewhere            
            workerThread.Start();

        }

        public static void RunAppTask(IAppTask appTask, int days)
        {
            // kickoff thread
            var originalPrincipal = Thread.CurrentPrincipal;

            Thread workerThread = new Thread(() =>
            {
                Thread.CurrentPrincipal = originalPrincipal;

                appTask.Run(days);

            });

            // kickoff the thread, log any result somewhere            
            workerThread.Start();

        }
    }
}
