using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD.Threading.Worker.AsyncTask
{
    public class AppTaskObject : IAppTask
    {
        public virtual void Run(string acctId, params object[] options)
        {
            throw new NotImplementedException();
        }

        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        public virtual void Run(int number)
        {
            throw new NotImplementedException();
        }
    }
}
