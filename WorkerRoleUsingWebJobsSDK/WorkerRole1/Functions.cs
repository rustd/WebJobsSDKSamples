using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerRole1
{
    static class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("inputqueueworkerrole")] string inputText,
        CancellationToken cancellationToken) 
         { 
             // Process Queue Message
             while (!cancellationToken.IsCancellationRequested)
             {
                 System.Diagnostics.Trace.TraceInformation("Function processing");
                 Thread.Sleep(1000);
             }
             System.Diagnostics.Trace.TraceInformation("Cancelled");
         } 
    }
}
