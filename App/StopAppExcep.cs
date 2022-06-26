using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projectUDT_app
{
    [Serializable]
    class StopAppExcep : Exception
    {
        public StopAppExcep()
        {
        }

        public StopAppExcep(string message)
            : base(message)
        {
        }

        public StopAppExcep(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}