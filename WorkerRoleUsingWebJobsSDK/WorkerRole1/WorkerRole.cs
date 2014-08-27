using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure.WebJobs;
using System.Configuration;

namespace WorkerRole1
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly JobHost host;

        public WorkerRole()
        {
            JobHostConfiguration hostConfig = new JobHostConfiguration()
            {
                DashboardConnectionString = CloudConfigurationManager.GetSetting("AzureWebJobsDashboard"),
                StorageConnectionString = CloudConfigurationManager.GetSetting("AzureWebJobsStorage")
            };
            host = new JobHost(hostConfig);
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();
            host.Start();
            Trace.TraceInformation("WorkerRole1 has been started");
            return result;
        }
        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRole1 is stopping");

            host.Stop();

            base.OnStop();

            Trace.TraceInformation("WorkerRole1 has stopped");
        }

    }
}
