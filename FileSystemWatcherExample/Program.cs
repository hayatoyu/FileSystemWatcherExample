using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;

namespace FileSystemWatcherExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }
        static void Run()
        {
            string watchedPath = System.Environment.CurrentDirectory + @"\watchedDirectory";
            FileSystemWatcher watcher = new FileSystemWatcher();

            if (!Directory.Exists(watchedPath))
                Directory.CreateDirectory(watchedPath);

            watcher.Path = watchedPath;            
            watcher.IncludeSubdirectories = true;
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press \'q\' to quit the sample");
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object source,FileSystemEventArgs e)
        {
            Console.WriteLine("File : " + e.FullPath + " " + e.ChangeType + "\n");
        }

        private static void OnRenamed(object source,RenamedEventArgs e)
        {
            Console.WriteLine("File : {0} renamed to {1}\n", e.OldFullPath, e.FullPath);
        }
    }
}
