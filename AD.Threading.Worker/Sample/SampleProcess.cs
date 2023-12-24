using AD.Threading.Worker.AsyncTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.Sample
{
    internal class SampleProcess : AppTaskObject
    {
        int _a, _b;
        internal SampleProcess(int a, int b)
        {
            _a = a;
            _b = b;
        }

        public override void Run()
        {
            ProcessJob();
        }

        public void ProcessJob()
        {
            // Do your thing.
        }
    }

    
}
