using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Jobs;
using System.Threading;

namespace FileWatcher
{
    // To learn more about Windows Azure WebJobs start here http://go.microsoft.com/fwlink/?LinkID=320976
    // This program wires up a FileWatcher and performs the following 2 operations
    // BackUpFile is called when a new File is detected in the path "c:\temp"
    class Program
    {
        private static JobHost host;

        static void Main(string[] args)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            FileSystemWatcher watcher = new FileSystemWatcher(@"C:\temp");
            watcher.Created += OnFileCreated;

            host = new JobHost();

            // Run the host on a background thread so we can invoke from dashboard
            host.RunOnBackgroundThread(cancelTokenSource.Token);

            // Enable the file watcher events and wait
            watcher.EnableRaisingEvents = true;
            while (!cancelTokenSource.IsCancellationRequested)
            {
                Thread.Sleep(2000);
            }
        }
       

        static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            string content = File.ReadAllText(e.FullPath);

            host.Call(typeof(Functions).GetMethod("BackupFile"),
                new
                {
                    fileContent = content,
                    fileName = Path.GetFileName(e.FullPath)
                });
        }
    }

}