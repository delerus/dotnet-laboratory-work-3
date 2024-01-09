using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lr3
{
    internal class Program
    {
        static void Main()
        {
            Mine mine = new Mine(300);
            Warhole warhole = new Warhole(mine);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            warhole.work();
            
            stopwatch.Stop();
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine($"\nProgram executed for: {timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds:000}");
        }
    }
}
