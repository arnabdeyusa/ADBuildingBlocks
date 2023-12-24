using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.AsyncTask
{
    public interface IAppTask
    {
        void Run(string acctId, params object[] options);

        void Run();

        void Run(int number);
    }
}
