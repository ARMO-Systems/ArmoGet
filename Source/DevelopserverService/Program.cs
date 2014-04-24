using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace ArmoSystems.ArmoGet.HaspClearService
{
    internal static class Program
    {
        private static void Main()
        {
            var newService = new DevelopserverService();
            try
            {
                RunWhileDebugging( newService );
                ServiceBase.Run( new ServiceBase[] { newService } );
            }
            catch ( Exception ex )
            {
                EventLog.WriteEntry( newService.ServiceName, ex.Message, EventLogEntryType.Error );
            }
        }

        [Conditional( "DEBUG" )]
        private static void RunWhileDebugging( DevelopserverService newService )
        {
            newService.DebugStart();
            //Thread.Sleep( 30 * 1000 );
            Thread.Sleep( 100 * 60000 );
            newService.DebugStop();
        }
    }
}