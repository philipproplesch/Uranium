using System;
using System.Diagnostics;

namespace Uranium.Core.Infrastructure.Bootstrapping
{
    class GlobalExceptionTracing : IBootstrapItem
    {
        public void Execute()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, e) => Trace.TraceError(e.ExceptionObject.ToString());
        }
    }
}
