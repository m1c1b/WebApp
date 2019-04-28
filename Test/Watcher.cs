using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace Test
{
    public class Watcher
    {
        public static void Threadstart()
        {
            Thread myThread = new Thread(Run);
            myThread.Start(); // запускаем поток
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void Run()
        {
            string args = $@"C:\Users\viazn\RiderProjects\WebApp\FormForCreatingValues\Values from the lab\{DateTime.Now.Year}\{DateTime.Now.Month}";

            // Create a new FileSystemWatcher and set its properties.
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = args;

                // Watch for changes in LastAccess and LastWrite times, and
                // the renaming of files or directories.
                watcher.NotifyFilter = NotifyFilters.LastAccess
                                       | NotifyFilters.LastWrite
                                       | NotifyFilters.FileName
                                       | NotifyFilters.DirectoryName;

                // Only watch text files.
                watcher.Filter = "*.txt";

                // Add event handlers.
                watcher.Changed += OnChanged;

                // Begin watching.
                watcher.EnableRaisingEvents = true;

                // Wait for the user to quit the program.
               // Console.WriteLine("Press 'q' to quit the sample.");
                while (true)
                {}
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //FillDb.Fill();
            Console.WriteLine("База была пополнена");
        }
        //Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

    }
}