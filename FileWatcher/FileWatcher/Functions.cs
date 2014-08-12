using Microsoft.Azure.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    static class Functions
    {

        [NoAutomaticTrigger]
        public static void ProcessFile(
            string fileContent,
            string fileName)
        {
            Console.WriteLine("Processing File: " + fileName);
        }


        [NoAutomaticTrigger]
        public static void BackupFile(
            string fileContent,
            string fileName,
            IBinder binder,
            TextWriter log)
        {
            log.WriteLine("Backing up file in Azure Storage: " + fileName);

            // Use IBinder because property binding doesn't work without a trigger attribute
            // if it would work, the code would be much simpler
            TextWriter backupFile = binder.Bind<TextWriter>(new BlobAttribute("backup/" + fileName));
            backupFile.Write(fileContent);
        }

    }
}
