using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    const int Iterations = 50;
    static void Main(string[] args)
    {
        // If launched as a trivial child process, exit immediately
        if (args.Length > 0 && args[0] == "--child-target")
        {
            return;
        }
        string targetExecutable = Environment.ProcessPath!;
        // 1. Measure Process Creation Overhead
        var processStopwatch = Stopwatch.StartNew();
        for (int i = 0; i < Iterations; i++)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = targetExecutable,
                UseShellExecute = false
            };
            startInfo.ArgumentList.Add("--child-target");

            using (Process? proc = Process.Start(startInfo))
            {
                proc?.WaitForExit();
            }
        }
        processStopwatch.Stop();
        // 2. Measure Thread Creation Overhead
        var threadStopwatch = Stopwatch.StartNew();
        for (int i = 0; i < Iterations; i++)
        {
            Thread t = new Thread(() => { /* trivial work */ });
            t.Start();
            t.Join();
        }
        threadStopwatch.Stop();
        // Calculate averages and ratio
        double avgProcessMs = processStopwatch.Elapsed.TotalMilliseconds / Iterations;
        double avgThreadMs = threadStopwatch.Elapsed.TotalMilliseconds / Iterations;
        Console.WriteLine($"Average process creation time: {avgProcessMs:F3} ms");
        Console.WriteLine($"Average thread creation time:  {avgThreadMs:F3} ms");
        Console.WriteLine($"Process creation was {(avgProcessMs / avgThreadMs):F1}x more expensive than thread creation.");
    }
}