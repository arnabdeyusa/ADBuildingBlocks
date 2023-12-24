using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.Sample
{
    internal class SampleTest
    {
        public static void RunTest()
        {
            // Call trigger to initiate the process.
            TriggerTask.ProcessSample(10, 12);
        }
    }
}
