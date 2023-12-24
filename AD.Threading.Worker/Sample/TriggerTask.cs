using AD.Threading.Worker.AsyncTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.Sample
{
    public static class TriggerTask
    {
        public static void ProcessSample(int a, int b)
        {
            // Initiating new worker thread
            AppTaskService.RunAppTask(new SampleProcess(a, b));
        }
    }
}
